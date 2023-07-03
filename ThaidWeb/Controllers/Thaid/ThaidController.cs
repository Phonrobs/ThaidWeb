using Microsoft.AspNetCore.Mvc;
using ThaidWeb.Exceptions;
using ThaidWeb.Services.Thaid;

namespace ThaidWeb.Controllers.AuthCallback;

[Route(ApiRoutes.Thaid.Route)]
[ApiController]
public class ThaidController : ControllerBase
{
    private readonly IThaidService _thaidService;

    public ThaidController(IThaidService thaidService)
    {
        _thaidService = thaidService;
    }

    [HttpGet(ApiRoutes.Thaid.Auth)]
    public ActionResult Authentication()
    {
        // get provider authentication endpoint URL
        var url = _thaidService.GetAuthEndpoint();

        // redirect to provider endpoint URL
        return Redirect(url);
    }

    [HttpGet]
    public async Task<ActionResult> AuthenticationCallback([FromQuery]string code, [FromQuery]string state)
    {
        // validated state from provider
        if (!await _thaidService.IsValidStateAsync(state))
        {
            throw new ThaidInvalidStateException();
        }

        // get token by using authorization code from provider
        var token = await _thaidService.GetTokenByAuthCodeAsync(code);

        if(token != null)
        {
             var key = await _thaidService.SaveTokenInCacheAsync(token);
            return Redirect($"~/UserInfo?key={key}");
        }

        return Redirect("~/InvalidResponse");
    }
}
