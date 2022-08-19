using Core.Wrappers;
using DataAccess.Abstract;
using DataAccess.Concrete.Enum;
using MediatR;

namespace Business.Handlers.Intern.Talent.Commands;

public class CreateTalentCommand:IRequest<IResponse>
{
    public string TalentName { get; set; }
    public string TalentExplanation { get; set; }
    public TalentLevelEnum TalentLevelEnum { get; set; }
    
    public class CreateTalentCommandHandler:IRequestHandler<CreateTalentCommand,IResponse>
    {
        private readonly ICurrentRepository _currentRepository;
        private readonly ITalentRepository _talentRepository;

        public CreateTalentCommandHandler(ICurrentRepository currentRepository, ITalentRepository talentRepository)
        {
            _currentRepository = currentRepository;
            _talentRepository = talentRepository;
        }

        public async Task<IResponse> Handle(CreateTalentCommand request, CancellationToken cancellationToken)
        {
            _currentRepository.UserControl(_currentRepository.UserId());

            
            
            var talent = new Entities.Concrete.Talent();
            talent.InternId = _currentRepository.UserId();
            talent.TalentName = request.TalentName;
            talent.TalentExplanation = request.TalentExplanation;
            talent.TalentLevelEnum = request.TalentLevelEnum;
            talent.isActive = true;

            _talentRepository.Add(talent);
            await _talentRepository.SaveChangesAsync();

            return new Response<Entities.Concrete.Talent>(talent);
        }
    }
}