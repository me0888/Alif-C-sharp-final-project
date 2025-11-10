using FluentValidation;
using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Validators;

public sealed class RiskDTOValidator : AbstractValidator<RiskCreateDTO>
{
    public RiskDTOValidator()
    {
        RuleFor(prop => prop.RiskProfileId).NotEmpty();
        RuleFor(prop => prop.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Type).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Description).NotEmpty().MinimumLength(5).MaximumLength(500);
        RuleFor(prop => prop.TimeFrame).GreaterThanOrEqualTo(1).LessThanOrEqualTo(3);
        RuleFor(prop => prop.Status).GreaterThanOrEqualTo(1).LessThanOrEqualTo(3);
    }
}
