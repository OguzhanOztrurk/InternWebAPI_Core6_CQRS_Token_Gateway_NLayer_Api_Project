using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Talent.Commands;

public class UpdateTalentCommand:IRequest<IResponse>
{
    public int TalentId { get; set; }
    public string TalentName { get; set; }
    public string TalentExplanation { get; set; }
    public TalentLevelEnum TalentLevelEnum { get; set; }
    
    public class UpdateTalentCommandHandler:IRequestHandler<UpdateTalentCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly ITalentRepository _talentRepository;

        public UpdateTalentCommandHandler(ICurrentRepository currentRepository, ITalentRepository talentRepository)
        {
            _currentRepository = currentRepository;
            _talentRepository = talentRepository;
        }

        public async Task<IResponse> Handle(UpdateTalentCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _talentRepository.TalentControl(request.TalentId,_currentRepository.UserId());

            var talent = await _talentRepository.GetAsync(x => x.TalentId == request.TalentId);
            talent.TalentName = request.TalentName;
            talent.TalentExplanation = request.TalentExplanation;
            talent.TalentLevelEnum = request.TalentLevelEnum;

            _talentRepository.Update(talent);
            await _talentRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Talent>(talent);
        }
    }
}