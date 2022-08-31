using Business.Handlers.Intern.Education.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Intern.Education.ValidationRules;

public class CreateEducationValidator : CustomValidator<CreateEducationCommand>
{
    public CreateEducationValidator()
    {
        RuleFor(x => x.SchoolName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.StartYear).MaximumLength(4).WithMessage("Maksimum 4 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EndYear).MaximumLength(4).WithMessage("Maksimum 4 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EducationLevelEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EducationStateEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteEducationValidator : CustomValidator<DeleteEducationCommand>
{
    public DeleteEducationValidator()
    {
        RuleFor(x => x.EducationId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateEducationValidator : CustomValidator<UpdateEducationCommand>
{
    public UpdateEducationValidator()
    {
        RuleFor(x => x.SchoolName).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.StartYear).MaximumLength(4).WithMessage("Maksimum 4 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EndYear).MaximumLength(4).WithMessage("Maksimum 4 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EducationLevelEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EducationStateEnum).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateEducationStateValidator : CustomValidator<UpdateEducationStateCommand>
{
    public UpdateEducationStateValidator()
    {
        RuleFor(x => x.EducationId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}