using Business.Handlers.Users.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Intern.Users.ValidationRules;

public class CreateUserInternValidator : CustomValidator<CreateUserInternCommand>
{
    public CreateUserInternValidator()
    {
        RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.PassWord).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteUserInternValidator : CustomValidator<DeleteUserInternCommand>
{
    public DeleteUserInternValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateInternPassValidator : CustomValidator<UpdateInternPassCommand>
{
    public UpdateInternPassValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.NewPasswordRety).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateUserInternValidator : CustomValidator<UpdateUserInternCommand>
{
    public UpdateUserInternValidator()
    {
        RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
    }
}