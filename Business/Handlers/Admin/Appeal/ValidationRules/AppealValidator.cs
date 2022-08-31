using Abp.Runtime.Validation.Interception;
using Business.Handlers.Admin.Appeal.Commands;
using Business.Handlers.Intern.Appeal.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Admin.Appeal.ValidationRules;

public class CreateAppealEvaluationValidator : CustomValidator<CreateAppealEvaluationCommand>
{
    public CreateAppealEvaluationValidator()
    {
        RuleFor(x => x.Conclusion).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.ConclusionDetail).MaximumLength(250).WithMessage("Maksimum 250 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.ApprovalStatus).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
    
}