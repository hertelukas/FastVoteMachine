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
            await SendOptionToGroup(id, option);
        }
    }

    private async Task SendOptionToGroup(int group, string option)
    {
        await Clients.Group(group.ToString()).SendAsync("AddedOption", option);
    }

    private async Task SendOptionToConnection(string connectionId, string option)
    {
        await Clients.Client(connectionId).SendAsync("AddedOption", option);
    }
    
    private async Task SendVotesToGroup(int id)
    {
        await Clients.Group(id.ToString()).SendAsync("UpdatedVotes", _voteHandler.GetVotes(id));
    }

    private async Task SendVotesToConnection(string connectionId, int id)
    {
        await Clients.Client(connectionId).SendAsync("UpdatedVotes", _voteHandler.GetVotes(id));
    }
    public async Task Connect(int id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());

        foreach (var option in _voteHandler.GetOptions(id))
        {
            await SendOptionToConnection(Context.ConnectionId, option);
        }

        await SendVotesToConnection(Context.ConnectionId, id);
        
        _voteHandler.Connect(id, Context.ConnectionId);
        await UpdateConnected(id);
    }

    public async Task Vote(int id, List<List<string>> votes)
    {
        foreach (var vote in votes)
        {
            // Ignore all votes that can not be parsed
            if (int.TryParse(vote[1], out var amount))
            {
                _voteHandler.VoteFor(id, vote[0], amount);
            }
        }

        await SendVotesToGroup(id);
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