using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Erm.BusinessLayer;
using Erm.PresentationLayer.WebApi.Authorization;

namespace Erm.PresentationLayer.WebApi;

public class Startup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(ISereviceCollection services)
	{
		services.RegisterBusinessLayerDependencies(Configuration);

		services.AddLogging(builder => builder.AddConsole());

		services.AddScoped<ApiKeyAuthFilter>();
		services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

		services.AddHttpContextAccessor();
		services.AddControllers();
		services.AddEndpointsApiExplorer();

		services.AddSwaggerGen(c =>
		{
			c.AddSecurityDefinition("API-Key", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Place to add API-Key",
				Name = "X-API-Key",

			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "API-Key"
						},
						Name = "API-Key",
					},
					new List<string>()
				}
			});
		});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseRouting();

		app.UseEndpoints(endpoints => endpoints.MapControllers());
	}

	public class AddCustomHeader : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			operation.Parameters.Add(new OpenApiParameter
			{
				Name = "X-API-Key",
				In = ParameterLocation.Header,
				Required = true,
			});
		}
	}
}