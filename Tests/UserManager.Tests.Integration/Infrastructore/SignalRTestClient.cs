using Microsoft.AspNetCore.SignalR.Client;

namespace UserManager.Tests.Integration.Infrastructore;

public static class SignalRTestClient
{
  public static HubConnection CreateHubConnection(HttpMessageHandler handler, string baseAddress, string hubPath)
  {
    return new HubConnectionBuilder()
      .WithUrl(new Uri(new Uri(baseAddress), hubPath), options =>
      {
        // Use the same in-memory server handler
        options.HttpMessageHandlerFactory = _ => handler;

        // If you use auth tokens instead, set AccessTokenProvider here
        // options.AccessTokenProvider = () => Task.FromResult("token");
      })
      .WithAutomaticReconnect()
      .Build();
  }
}
