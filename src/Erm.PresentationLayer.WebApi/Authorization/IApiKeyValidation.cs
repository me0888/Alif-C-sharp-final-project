namespace Erm.PresentationLayer.WebApi.Authorization;

public interface IApiKeyValidation
{
    bool IsValidApiKey(string userApiKey);
}


