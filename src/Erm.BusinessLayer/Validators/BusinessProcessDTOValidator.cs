using FluentValidation;
using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Validators;

public sealed class BusinessProcessDTOValidator : AbstractValidator<BusinessProcessCreateDTO>
{
    public BusinessProcessDTOValidator()
    {
        RuleFor(prop => prop.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Domain).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}

