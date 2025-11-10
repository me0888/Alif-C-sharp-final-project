using Microsoft.AspNetCore.Mvc;

namespace Erm.PresentationLayer.WebApi.Authorization;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter))
    {
    }
}


