using Business.Handlers.Intern.Appeal.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Intern.Appeal.ValidationRules;

public class CreateAppealValidator : CustomValidator<CreateAppealCommand>
{
    public CreateAppealValidator()
    {
        RuleFor(x => x.AdvertId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteAppealValidator : CustomValidator<DeleteAppealCommand>
{
    public DeleteAppealValidator()
    {
        RuleFor(x => x.AppealId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateAppealStateValidator : CustomValidator<UpdateAppealStateCommand>
{
    public UpdateAppealStateValidator()
    {
        RuleFor(x => x.AppealId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}