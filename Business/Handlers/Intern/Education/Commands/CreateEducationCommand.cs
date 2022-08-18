using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Education.Commands;

public class CreateEducationCommand:IRequest<IResponse>
{
    public string SchoolName { get; set; }
    public EducationLevelEnum EducationLevelEnum { get; set; }
    public string StartYear { get; set; }
    public string EndYear { get; set; }
    public EducationStateEnum EducationStateEnum { get; set; }
    
    public class CreateEducationCommandHandler:IRequestHandler<CreateEducationCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IEducationRepository _educationRepository;

        public CreateEducationCommandHandler(ICurrentRepository currentRepository, IEducationRepository educationRepository)
        {
            _currentRepository = currentRepository;
            _educationRepository = educationRepository;
        }

        public async Task<IResponse> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            var education = new Entities.Concrete.Education();
            education.InternId = _currentRepository.UserId();
            education.SchoolName = request.SchoolName;
            education.EducationLevelEnum = request.EducationLevelEnum;
            education.StartYear = request.StartYear;
            education.EndYear = request.EndYear;
            education.EducationStateEnum = request.EducationStateEnum;
            education.isActive = true;

            _educationRepository.Add(education);
            await _educationRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Education>(education);
        }
    }
}