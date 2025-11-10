using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Erm.BusinessLayer.Validators;
using Microsoft.Extensions.Configuration;
using Erm.DataAccess;
using Erm.BusinessLayer.Mapper;
using Erm.BusinessLayer.Service;
using Erm.BusinessLayer.DTO;


namespace Erm.BusinessLayer;

public static class DependencyInjection
{
	public static void RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAutoMapper(options
			=> options.AddProfile<AutoMapperProfiles>());

		services.AddScoped<IRiskProfileService, RiskProfileService>();
		services.AddScoped<IValidator<RiskProfileCreateDTO>, RiskProfileDTOValidator>();
		services.AddScoped<IValidator<RiskProfileUpdateDTO>, RiskProfileUpdateDTOValidator>();

		services.AddScoped<IBusinessProcessService, BusinessProcessService>();
		services.AddScoped<IValidator<BusinessProcessCreateDTO>, BusinessProcessDTOValidator>();
		services.AddScoped<IValidator<BusinessProcessUpdateDTO>, BusinessProcessUpdateDTOValidator>();

		services.AddScoped<IRiskService, RiskService>();
		services.AddScoped<IValidator<RiskCreateDTO>, RiskDTOValidator>();
		services.AddScoped<IValidator<RiskUpdateDTO>, RiskUpdateDTOValidator>();

		services.AddScoped<INotificationService, NotificationService>();

		services.RegisterDataAccessDependencies(configuration);
		
	}
}