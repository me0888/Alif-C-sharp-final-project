using FluentValidation;

using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Validators;

public sealed class RiskProfileUpdateDTOValidator : AbstractValidator<RiskProfileUpdateDTO>
{
    public RiskProfileUpdateDTOValidator()
    {
        RuleFor(prop => prop.Name).MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Description).MinimumLength(5).MaximumLength(500);
        RuleFor(prop => prop.BusinessProcessId);
        RuleFor(prop => prop.OccurrenceProbability).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
        RuleFor(prop => prop.PotentialBusinessImpact).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
    }
}

