using Business.Handlers.Intern.WorkHistory.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Intern.WorkHistory.ValidationRules;

public class CreateWorkHistoryValidator : CustomValidator<CreateWorkHistoryCommand>
{
    public CreateWorkHistoryValidator()
    {
        RuleFor(x => x.WorkplaceName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.OperationTime).MaximumLength(30).WithMessage("Maksimum 30 karakter olmalıdır").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkStateEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteWorkHistoryValidator : CustomValidator<DeleteWorkHistoryCommand>
{
    public DeleteWorkHistoryValidator()
    {
        RuleFor(x => x.WorkHistoryId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateWorkHistoryValidator : CustomValidator<UpdateWorkHistoryCommand>
{
    public UpdateWorkHistoryValidator()
    {
        RuleFor(x => x.WorkplaceName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.OperationTime).MaximumLength(30).WithMessage("Maksimum 30 karakter olmalıdır").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkStateEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateWorkHistoryStateValidator : CustomValidator<UpdateWorkHistoryStateCommand>
{
    public UpdateWorkHistoryStateValidator()
    {
        RuleFor(x => x.WorkHistoryId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}