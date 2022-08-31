using Abp.Runtime.Validation.Interception;
using Business.Handlers.Intern.Talent.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Intern.Talent.ValidationRules;

public class CreateTalentValidator : CustomValidator<CreateTalentCommand>
{
    public CreateTalentValidator()
    {
        RuleFor(x => x.TalentName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.TalentExplanation).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.TalentLevelEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteTalentValidator : CustomValidator<DeleteTalentCommand>
{
    public DeleteTalentValidator()
    {
        RuleFor(x => x.TalenId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateTalentValidator : CustomValidator<UpdateTalentCommand>
{
    public UpdateTalentValidator()
    {
        RuleFor(x => x.TalentName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.TalentExplanation).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.TalentLevelEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateTalentStateValdiator : CustomValidator<UpdateTalentStateCommand>
{
    public UpdateTalentStateValdiator()
    {
        RuleFor(x => x.TalentId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}