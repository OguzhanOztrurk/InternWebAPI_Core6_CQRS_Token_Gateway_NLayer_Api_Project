using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Education.Commands;

public class UpdateEducationStateCommand:IRequest<IResponse>
{
    public int EducationId { get; set; }
    public class UpdateEducationStateCommandHandler:IRequestHandler<UpdateEducationStateCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IEducationRepository _educationRepository;

        public UpdateEducationStateCommandHandler(ICurrentRepository currentRepository, IEducationRepository educationRepository)
        {
            _currentRepository = currentRepository;
            _educationRepository = educationRepository;
        }

        public async Task<IResponse> Handle(UpdateEducationStateCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _educationRepository.EducationControl(request.EducationId,_currentRepository.UserId());

            var education = await _educationRepository.GetAsync(x => x.EducationId == request.EducationId);
            education.isActive = !education.isActive;

            _educationRepository.Update(education);
            await _educationRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Education>(education);
        }
    }
}