using Abp.Runtime.Validation.Interception;
using Business.Handlers.Advert.Command;
using Core.Validation;
using FluentValidation;

namespace Business.Handlers.Admin.Advert.ValidationRules;

public class CreateAdvertValidator:CustomValidator<CreateAdvertCommand>
{
    public CreateAdvertValidator()
    {
    }
}