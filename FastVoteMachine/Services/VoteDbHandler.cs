using FastVoteMachine.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastVoteMachine.Services;

// TODO Error handling
// TODO async?
public class VoteDbHandler : IVoteHandler
{
    private readonly AppDbContext _context;

    public VoteDbHandler(AppDbContext context)
    {
        _context = context;
    }

    private VoteEntity _getVote(int id)
    {
        var vote = _context.Votes.Find(id);

        if (vote == null)
        {
            throw new Exception("Could not find vote!");
        }

        return vote;
    }

    public void VoteFor(int id, string option, int amount)
    {
        var options = _context.Votes
            .Where(v => v.VoteEntityId == id)
            .Select(v => v.Options.ToList())
            .Single();

        foreach (var tmp in options)
        {
            if (tmp.Name.Equals(option))
                tmp.Votes += amount;
        }

        _context.SaveChanges();
    }

    public int CreateVoting(string name)
    {
        var vote = new VoteEntity {Name = name};
        _context.Add(vote);
        _context.SaveChanges();
        return vote.VoteEntityId;
    }

    public bool AddOption(int id, string option)
    {
        var vote = _getVote(id);

        // Check whether the option exists
        if (vote.Options != null && vote.Options.Any(o => o.Name.Equals(option)))
        {
            return false;
        }

        vote.Options ??= new List<OptionEntity>();

        // If it does not exist, add it
        vote.Options.Add(new OptionEntity {Name = option, Votes = 0});

        _context.SaveChanges();
        return true;
    }

    public List<string> GetOptions(int id)
    {
        return _context.Votes
            .Where(v => v.VoteEntityId == id)
            .Select(v => v.Options.Select(o => o.Name).ToList())
            .Single();
    }

    public Dictionary<string, int> GetVotes(int id)
    {
        var options = _context.Votes
            .Where(v => v.VoteEntityId == id)
            .Select(v => v.Options)
            .First();

        return options == null
            ? new Dictionary<string, int>()
            : options.ToDictionary(x => x.Name, x => x.Votes);
    }

    public string GetName(int id)
    {
        return _context.Votes
            .Where(v => v.VoteEntityId == id)
            .Select(v => v.Name)
            .Single();
    }

    public int GetConnected(int id)
    {
        return _context.Votes
            .Where(v => v.VoteEntityId == id)
            .Select(v => v.Connections.Count)
            .Single();
    }

    public void Connect(int id, string connection)
    {
        var c = _context.Connections.FirstOrDefault(c => c.Connection.Equals(connection));

        var vote = _getVote(id);

        // Every connection has at least one vote
        if (c == null)
        {
            var votes = new List<VoteEntity> {vote};
            c = new ConnectionEntity {Connection = connection, Votes = votes};

            _context.Connections.Add(c);
        }
        else
        {
            c.Votes.Add(vote);
        }

        // A vote can have 0 connections
        if (vote.Connections == null)
        {
            vote.Connections = new List<ConnectionEntity> {c};
        }
        else
        {
            vote.Connections.Add(c);
        }

        _context.SaveChanges();
    }

    public HashSet<int> Disconnect(string connection)
    {
        var c = _context.Connections.Include(c => c.Votes).FirstOrDefault(c => c.Connection.Equals(connection));
        if (c == null)
        {
            return new HashSet<int>();
        }

        var joinedVotes = c.Votes;
        var result = joinedVotes.AsQueryable().Select(v => v.VoteEntityId).ToHashSet();

        if (joinedVotes is {Count: > 0})
        {
            foreach (var vote in joinedVotes)
            {
                vote.Connections.Remove(c);
            }
        }
        else
        {
            return new HashSet<int>();
        }

        _context.Connections.Remove(c);

        _context.SaveChanges();
        return result;
    }
}