using FluentValidation;
using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Validators;

public sealed class BusinessProcessUpdateDTOValidator : AbstractValidator<BusinessProcessUpdateDTO>
{
    public BusinessProcessUpdateDTOValidator()
    {
        RuleFor(prop => prop.Name).MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Domain).MinimumLength(3).MaximumLength(50);
    }
}

