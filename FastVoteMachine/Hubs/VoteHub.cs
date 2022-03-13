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

    public async Task AddOption(int id, string option)
    {
        if (_voteHandler.AddOption(id, option))
        {
            await SendOptionToEveryone(id, option);
        }
    }

    private async Task SendOptionToEveryone(int group, string option)
    {
        await Clients.Group(group.ToString()).SendAsync("AddedOption", option);
    }

    private async Task SendOptionToConnection(string connectionId, string option)
    {
        await Clients.Client(connectionId).SendAsync("AddedOption", option);
    }
    public async Task Connect(int id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());

        foreach (var option in _voteHandler.GetOptions(id))
        {
            await SendOptionToConnection(Context.ConnectionId, option);
        }
        
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