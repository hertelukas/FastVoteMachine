namespace FastVoteMachine;

/// <summary>
/// A simple (not thread safe) implementation for storing information about a vote
/// </summary>
public class Vote
{
    public Dictionary<string, int> Options { get; }
    public int Connected { get; private set; }
    public string Name { get; }
    public Vote(string name)
    {
        Options = new Dictionary<string, int>();
        Connected = 0;
        Name = name;
    }

    public bool AddOption(string option)
    {
        return Options.TryAdd(option, 0);
    }

    public void VoteFor(string option, int amount)
    {
        if (Options.ContainsKey(option))
        {
            Options[option] += amount;
        }
    }

    public void Connect()
    {
        Connected++;
    }

    public void Disconnect()
    {
        Connected--;
    }
}