using FluentValidation;
using Erm.BusinessLayer.DTO;

namespace Erm.BusinessLayer.Validators;

public sealed class RiskProfileDTOValidator : AbstractValidator<RiskProfileCreateDTO>
{
    public RiskProfileDTOValidator()
    {
        RuleFor(prop => prop.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(prop => prop.Description).NotEmpty().MinimumLength(5).MaximumLength(500);
        RuleFor(prop => prop.BusinessProcessId).NotEmpty();
        RuleFor(prop => prop.OccurrenceProbability).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
        RuleFor(prop => prop.PotentialBusinessImpact).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
    }
}

