using Abp.Runtime.Validation.Interception;
using Business.Handlers.Advert.Command;
using Core.Constants;
using Core.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace Business.Handlers.Admin.Advert.ValidationRules;

public class CreateAdvertValidator:CustomValidator<CreateAdvertCommand>
{
    public CreateAdvertValidator()
    {
        RuleFor(x => x.AdvertName).MaximumLength(100).WithMessage("Maksimum 100 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.AdvertSummary).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalı.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EndDate).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Quota).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        
        RuleFor(x => x.CompanyInfo).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalı.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkDefinition).MaximumLength(250).WithMessage("Maksimum 250 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Quality).MaximumLength(400).WithMessage("Maksimum 400 karakter olamlıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkEnvironment).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkHour).MaximumLength(200).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Facilities).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Wage).MaximumLength(16).WithMessage("Maksimum 16 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        
    }
    
}

public class DeleteAdvertValidator : CustomValidator<DeleteAdvertCommand>
{
    public DeleteAdvertValidator()
    {
        RuleFor(x => x.AdvertId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}

public class UpdateAdvertValidator : CustomValidator<UpdateAdvertCommand>
{
    public UpdateAdvertValidator()
    {
        RuleFor(x => x.AdvertName).MaximumLength(100).WithMessage("Maksimum 100 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.AdvertSummary).MaximumLength(200).WithMessage("Maksimum 200 karakter olmalı.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.EndDate).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Quota).NotEmpty().WithMessage(ValidatorMessage.IsNull);
        
        RuleFor(x => x.CompanyInfo).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalı.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkDefinition).MaximumLength(250).WithMessage("Maksimum 250 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Quality).MaximumLength(400).WithMessage("Maksimum 400 karakter olamlıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkEnvironment).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.WorkHour).MaximumLength(200).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Facilities).MaximumLength(400).WithMessage("Maksimum 400 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
        RuleFor(x => x.Wage).MaximumLength(16).WithMessage("Maksimum 16 karakter olmalıdır.").NotEmpty()
            .WithMessage(ValidatorMessage.IsNull);
    }
}

public class StateAdvertValidator : CustomValidator<StateAdvertCommand>
{
    public StateAdvertValidator()
    {
        RuleFor(x => x.AdvertId).NotEmpty().WithMessage(ValidatorMessage.IsNull);
    }
}