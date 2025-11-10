using FluentValidation;

using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Validators;

public sealed class RiskUpdateDTOValidator : AbstractValidator<RiskUpdateDTO>
{
    public RiskUpdateDTOValidator()
    {
        RuleFor(prop => prop.Name).MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Type).MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Description).MinimumLength(5).MaximumLength(500);
        RuleFor(prop => prop.TimeFrame).GreaterThanOrEqualTo(1).LessThanOrEqualTo(3);
        RuleFor(prop => prop.Status).GreaterThanOrEqualTo(1).LessThanOrEqualTo(3);
    }
}
