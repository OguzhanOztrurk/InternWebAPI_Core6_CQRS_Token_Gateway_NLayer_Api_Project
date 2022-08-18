using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Education.Commands;

public class UpdateEducationCommand:IRequest<IResponse>
{
    public int EducationId { get; set; }
    public string SchoolName { get; set; }
    public EducationLevelEnum EducationLevelEnum { get; set; }
    public string StartYear { get; set; }
    public string EndYear { get; set; }
    public EducationStateEnum EducationStateEnum { get; set; }
    public class UpdateEducationCommandHandler:IRequestHandler<UpdateEducationCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IEducationRepository _educationRepository;

        public UpdateEducationCommandHandler(ICurrentRepository currentRepository, IEducationRepository educationRepository)
        {
            _currentRepository = currentRepository;
            _educationRepository = educationRepository;
        }

        public async Task<IResponse> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _educationRepository.EducationControl(request.EducationId,_currentRepository.UserId());

            var education = await _educationRepository.GetAsync(x => x.EducationId == request.EducationId);
            education.SchoolName = request.SchoolName;
            education.EducationLevelEnum = request.EducationLevelEnum;
            education.EducationStateEnum = request.EducationStateEnum;
            education.StartYear = request.StartYear;
            education.EndYear = request.EndYear;

            _educationRepository.Update(education);
            await _educationRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Education>(education);
        }
    }
}