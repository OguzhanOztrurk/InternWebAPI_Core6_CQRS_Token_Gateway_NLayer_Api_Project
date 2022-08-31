using Business.Handlers.Admin.AppealEvaluation.Commands;
using Business.Handlers.Intern.AppealEvaluation.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Intern.AppealEvaluation.ValidationRules;

public class DeleteAppealEvaluationValidator : CustomValidator<DeleteAppealEvaluationCommand>
{
    public DeleteAppealEvaluationValidator()
    {
        RuleFor(x => x.AppealId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateAppealEvaluationValidator : CustomValidator<UpdateAppealEvulationCommand>
{
    public UpdateAppealEvaluationValidator()
    {
        RuleFor(x => x.AppealId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.AppealEvulationStatus).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}