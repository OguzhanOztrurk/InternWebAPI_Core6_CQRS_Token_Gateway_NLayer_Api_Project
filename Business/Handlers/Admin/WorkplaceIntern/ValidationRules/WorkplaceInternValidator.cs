using Business.Handlers.Admin.WorkplaceIntern.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Admin.WorkplaceIntern.ValidationRules;

public class DeleteWorkplaceInternValidator : CustomValidator<DeleteWorkplaceInternCommand>
{
    public DeleteWorkplaceInternValidator()
    {
        RuleFor(x => x.WorkplaceInterId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        
    }
}

public class UpdateWorkplaceInternStateValidator : CustomValidator<UpdateWorkplaceInternStateCommand>
{
    public UpdateWorkplaceInternStateValidator()
    {
        RuleFor(x => x.workplaceInternId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkplaceInternState).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        
    }
}