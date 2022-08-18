using Core.Wrappers;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Intern.Talent.Commands;

public class UpdateTalentStateCommand:IRequest<IResponse>
{
    public int TalentId { get; set; }
    
    public class UpdateTalentStateCommandHandler:IRequestHandler<UpdateTalentStateCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly ITalentRepository _talentRepository;

        public UpdateTalentStateCommandHandler(ICurrentRepository currentRepository, ITalentRepository talentRepository)
        {
            _currentRepository = currentRepository;
            _talentRepository = talentRepository;
        }

        public async Task<IResponse> Handle(UpdateTalentStateCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());
            _talentRepository.TalentControl(request.TalentId,_currentRepository.UserId());

            var talent = await _talentRepository.GetAsync(x => x.TalentId == request.TalentId);
            talent.isActive = !talent.isActive;
            
            _talentRepository.Update(talent);
            await _talentRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Talent>(talent);
        }
    }
}