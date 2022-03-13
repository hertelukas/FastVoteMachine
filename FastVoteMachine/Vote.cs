namespace FastVoteMachine;

// This class represents one ongoing vote
public class Vote
{
    public Dictionary<string, int> Options { get; }
    public int Connected { get; private set; }

    public Vote()
    {
        Options = new Dictionary<string, int>();
        Connected = 0;
    }

    public void AddOption(string option)
    {
        Options.TryAdd(option, 0);
    }

    public void VoteFor(string option)
    {
        if (Options.ContainsKey(option))
        {
            Options[option]++;
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