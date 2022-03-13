using Microsoft.AspNetCore.SignalR;

namespace FastVoteMachine.Hubs;

public class ChatHub : Hub
{
   public async Task SendMessage(string user, string message)
   {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
   }

   public async Task SendConnected()
   {
      await Clients.All.SendAsync("ClientConnected");
   }
}