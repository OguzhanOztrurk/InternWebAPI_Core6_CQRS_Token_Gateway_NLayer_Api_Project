using Business.Handlers.Workplace.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Admin.Workplace.ValidationRules;

public class CreateWorkplaceValidator : CustomValidator<CreateWorkplaceCommand>
{
    public CreateWorkplaceValidator()
    {
        RuleFor(x => x.WorkplaceName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkplaceExplanation).MaximumLength(2000).WithMessage("Maksimum 2000 karakter olmalıdır.")
            .NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EmployeesCount).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Vision).MaximumLength(600).WithMessage("Maksimum 600 karakter olmalıdır").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Mission).MaximumLength(600).WithMessage("Maksimum 600 karakter olmalıdır").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteWorkplaceValidator : CustomValidator<DeleteWorkplaceCommand>
{
    public DeleteWorkplaceValidator()
    {
        RuleFor(x => x.WorkplaceId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class StateWorkplaceValidator : CustomValidator<StateWorkplaceCommand>
{
    public StateWorkplaceValidator()
    {
        RuleFor(x => x.WorkplaceId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateWorkplaceValidator : CustomValidator<UpdateWorkplaceCommand>
{
    public UpdateWorkplaceValidator()
    {
        RuleFor(x => x.WorkplaceName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkplaceExplanation).MaximumLength(2000).WithMessage("Maksimum 2000 karakter olmalıdır.")
            .NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EmployeesCount).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Vision).MaximumLength(600).WithMessage("Maksimum 600 karakter olmalıdır").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Mission).MaximumLength(600).WithMessage("Maksimum 600 karakter olmalıdır").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
    }
}