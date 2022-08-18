using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Talent.Commands;

public class DeleteTalentCommand:IRequest<IResponse>
{
    public int TalenId { get; set; }
    
    public class DeleteTalentCommandHandler:IRequestHandler<DeleteTalentCommand, IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly ITalentRepository _talentRepository;

        public DeleteTalentCommandHandler(ICurrentRepository currentRepository, ITalentRepository talentRepository)
        {
            _currentRepository = currentRepository;
            _talentRepository = talentRepository;
        }

        public async Task<IResponse> Handle(DeleteTalentCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _talentRepository.TalentControl(request.TalenId,_currentRepository.UserId());

            var talent = await _talentRepository.GetAsync(x => x.TalentId == request.TalenId);
            talent.DeleteDate=DateTime.Now;
            talent.DeleteUserId = _currentRepository.UserId();

            _talentRepository.Update(talent);
            await _talentRepository.SaveChangesAsync();

            return new Response<object>(null,"Has been deleted");
        }
    }
}