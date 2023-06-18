using Microsoft.AspNetCore.Mvc;
using Obs.API.Contracts.Requests;
using Obs.API.Contracts.Responses;
using Obs.API.Contracts.V1;
using Obs.Application.Services.IdentityService;

namespace Obs.API.Controllers.V1;

public class IdentityController : BaseController
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost(ApiRoutes.Identity.Register)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthFailedResponse
            {
                Errors = ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage))
            });
        }

        var authReponse = await _identityService.RegisterAsync(request.Email, request.Password);

        if (!authReponse.Success)
        {
            return BadRequest(new AuthFailedResponse
            {
                Errors = authReponse.Errors
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Token = authReponse.Token,
            RefreshToken = authReponse.RefreshToken
        });
    }

    [HttpPost(ApiRoutes.Identity.Login)]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token,
            RefreshToken = authResponse.RefreshToken
        });
    }

    [HttpPost(ApiRoutes.Identity.Refresh)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

        if (!authResponse.Success)
        {
            return BadRequest(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });
        }

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token,
            RefreshToken = authResponse.RefreshToken
        });
    }
}