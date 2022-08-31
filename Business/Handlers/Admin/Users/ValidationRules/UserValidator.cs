using Business.Handlers.Users.Commands;
using Core.Constants;
using Core.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace Business.Handlers.Admin.Users.ValidationRules;

public class CreateUserAdminValidator : CustomValidator<CreateUserAdminCommand>
{
    public CreateUserAdminValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Maksimum 50 karakter olamlıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Maksimum 50 karakter olamalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.PassWord).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class DeleteUserAdminValidator : CustomValidator<DeleteUserAdminCommand>
{
    public DeleteUserAdminValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateAdminPassValidator : CustomValidator<UpdateAdminPassCommand>
{
    public UpdateAdminPassValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.NewPasswordRety).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateUserAdminValidator : CustomValidator<UpdateUserAdminCommand>
{
    public UpdateUserAdminValidator()
    {
        RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalı.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Number).MaximumLength(11).WithMessage("Maksimum 11 karakter olmalıdır.");
        RuleFor(x => x.Email).MaximumLength(150).WithMessage("Maksimum 150 karakter olmalıdır.");
        RuleFor(x => x.Position).MaximumLength(150).WithMessage("Maksimum 150 karakter olmalıdır.");
    }
}