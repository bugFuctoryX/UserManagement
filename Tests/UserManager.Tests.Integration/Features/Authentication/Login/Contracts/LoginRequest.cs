namespace UserManager.Tests.Integration.Features.Authentication.Login.Contracts;

public sealed record LoginRequest(string UserName, string Password);