using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erm.PresentationLayer.WebApi.Authorization;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
    {
        _apiKeyValidation = apiKeyValidation;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName].ToString();

        if (string.IsNullOrWhiteSpace(userApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!_apiKeyValidation.IsValidApiKey(userApiKey))
        {
            context.Result = new UnauthorizedResult();
        }

    }
}


