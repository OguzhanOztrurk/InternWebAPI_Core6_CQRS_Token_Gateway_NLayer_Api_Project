using Core.Wrappers;
using MediatR;

namespace Business.Handlers.Intern.AppealEvaluation.Commands;

public class CreateAppealEvulationCommand:IRequest<IResponse>
{
    public class CreateAppealEvulationCommandHandler:IRequestHandler<CreateAppealEvulationCommand, IResponse>
    {
        public Task<IResponse> Handle(CreateAppealEvulationCommand request, CancellationToken cancellationToken)
        {
            throw new Exception();
        }
    }
}