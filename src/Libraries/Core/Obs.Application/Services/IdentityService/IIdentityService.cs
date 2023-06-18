﻿using Obs.Domain.Models;

namespace Obs.Application.Services.IdentityService;

public interface IIdentityService
{
    Task<AuthenticationResult> RegisterAsync(string email, string password);
    Task<AuthenticationResult> LoginAsync(string email, string password);
    Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
}