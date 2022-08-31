using Abp.Runtime.Validation.Interception;
using Business.Handlers.Admin.AppealEvaluation.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Admin.AppealEvaluation.ValidationRules;

public class DeleteAppealEvaluationValidator : CustomValidator<DeleteAppealEvaluationCommand>
{
    public DeleteAppealEvaluationValidator()
    {
        RuleFor(x => x.AppealId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}