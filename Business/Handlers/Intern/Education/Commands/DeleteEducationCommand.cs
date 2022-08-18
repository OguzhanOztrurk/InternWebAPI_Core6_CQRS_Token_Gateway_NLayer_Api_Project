using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Education.Commands;

public class DeleteEducationCommand:IRequest<IResponse>
{
    public int EducationId { get; set; }
    
    public class DeleteEducationCommandHandler:IRequestHandler<DeleteEducationCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly IEducationRepository _educationRepository;

        public DeleteEducationCommandHandler(ICurrentRepository currentRepository, IEducationRepository educationRepository)
        {
            _currentRepository = currentRepository;
            _educationRepository = educationRepository;
        }

        public async Task<IResponse> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _educationRepository.EducationControl(request.EducationId, _currentRepository.UserId());

            var education = await _educationRepository.GetAsync(x => x.EducationId == request.EducationId);
            education.DeleteDate=DateTime.Now;
            education.DeleteUserId = _currentRepository.UserId();

            _educationRepository.Update(education);
            await _educationRepository.SaveChangesAsync();

            return new Response<object>(null, "Has been deleted");
        }
    }
}