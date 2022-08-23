using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Intern.AppealEvaluation.Commands;

public class UpdateAppealEvulationCommand:IRequest<IResponse>
{
    public int  AppealId { get; set; }
    public bool AppealEvulationStatus { get; set; }
    public class UpdateAppealEvulationCommandHandler:IRequestHandler<UpdateAppealEvulationCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IAppealEvaluationRepository _appealEvaluationRepository;
        private readonly IWorkplaceInternRepository _workplaceInternRepository;
        private readonly IAppealRepository _appealRepository;

        public UpdateAppealEvulationCommandHandler(ICurrentRepository currentRepository, IAppealEvaluationRepository appealEvaluationRepository, IWorkplaceRepository workplaceRepository, IWorkplaceInternRepository workplaceInternRepository, IAppealRepository appealRepository)
        {
            _currentRepository = currentRepository;
            _appealEvaluationRepository = appealEvaluationRepository;
            _workplaceInternRepository = workplaceInternRepository;
            _appealRepository = appealRepository;
        }

        public async Task<IResponse> Handle(UpdateAppealEvulationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _appealEvaluationRepository.AppealEvaluationInternConfirmControl(request.AppealId, _currentRepository.UserId());
            _appealRepository.InternStudyStateControl(_currentRepository.UserId());
            var getAdvertId = await _appealRepository.GetAsync(x => x.AppealId == request.AppealId);
            _appealRepository.InternAppealQuotaControl(getAdvertId.AdvertId);

            var appealEvuluationEnum = EvaluationStateEnum.Waiting;
            if (request.AppealEvulationStatus)
            {
                appealEvuluationEnum = EvaluationStateEnum.Approved;
            }
            else if(!request.AppealEvulationStatus)
            {
                appealEvuluationEnum = EvaluationStateEnum.Denied;
            }

            var appealevulation = await _appealEvaluationRepository.GetAsync(x => x.AppealId == request.AppealId);
            appealevulation.EvaluationStateEnum = appealEvuluationEnum;

            _appealEvaluationRepository.Update(appealevulation);
            await _appealEvaluationRepository.SaveChangesAsync();



            var advertId = _appealEvaluationRepository.GetAdvertId(request.AppealId);
            var workplaceIntern = new WorkplaceIntern();
            workplaceIntern.WorkplaceId = _appealEvaluationRepository.GetAppealWorkplaceId(request.AppealId);
            workplaceIntern.InternId = _currentRepository.UserId();
            workplaceIntern.AcceptDate=DateTime.Now;
            workplaceIntern.isActive = true;
            workplaceIntern.AdvertId = advertId;

            _workplaceInternRepository.Add(workplaceIntern);
            await _workplaceInternRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.AppealEvaluation>(appealevulation);

        }
    }
}