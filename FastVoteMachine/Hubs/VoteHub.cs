using FastVoteMachine.Services;
using Microsoft.AspNetCore.SignalR;

namespace FastVoteMachine.Hubs;

public class VoteHub : Hub
{
    private readonly IVoteHandler _voteHandler;

    public VoteHub(IVoteHandler voteHandler)
    {
        _voteHandler = voteHandler;
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task Connect(int id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());

        _voteHandler.Connect(id, Context.ConnectionId);
        await UpdateConnected(id);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        foreach (var group in _voteHandler.Disconnect(Context.ConnectionId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.ToString());
            await UpdateConnected(group);
        }

        await base.OnDisconnectedAsync(exception);
    }


    private async Task UpdateConnected(int group)
    {
        await Clients.Group(group.ToString()).SendAsync("UpdateConnected", _voteHandler.GetConnected(group));
    }
}