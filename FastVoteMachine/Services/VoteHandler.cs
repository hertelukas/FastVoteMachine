namespace FastVoteMachine.Services;

public class VoteHandler : IVoteHandler
{
    private readonly List<Vote> _votes;

    private readonly Dictionary<string, HashSet<int>> _joinedGroups = new();

    public VoteHandler()
    {
        _votes = new List<Vote>();
    }

    public void VoteFor(int id, string option)
    {
        _votes[id].VoteFor(option);
    }

    public int CreateVoting()
    {
        int index = _votes.Count;
        _votes.Add(new Vote());
        return index;
    }

    public Dictionary<string, int> GetVotes(int id)
    {
        return _votes[id].Options;
    }

    public int GetConnected(int id)
    {
        return _votes[id].Connected;
    }

    public void Connect(int id, string connection)
    {
        _joinedGroups.TryAdd(connection, new HashSet<int>());
        _joinedGroups[connection].Add(id);
        _votes[id].Connect();
    }

    public HashSet<int> Disconnect(string connection)
    {
        var groups = _joinedGroups[connection];
        foreach (var group in groups)
        {
            _votes[group].Disconnect();
        }
        _joinedGroups.Remove(connection);
        return groups;
    }

}