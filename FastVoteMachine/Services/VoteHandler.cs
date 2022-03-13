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

    public int CreateVoting(string name)
    {
        var index = _votes.Count;
        _votes.Add(new Vote(name));
        return index;
    }

    public bool AddOption(int id, string option)
    {
        return _votes[id].AddOption(option);
    }

    public List<string> GetOptions(int id)
    {
        return new List<string>(_votes[id].Options.Keys);
    }

    public Dictionary<string, int> GetVotes(int id)
    {
        return _votes[id].Options;
    }

    public string GetName(int id)
    {
        return _votes[id].Name;
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