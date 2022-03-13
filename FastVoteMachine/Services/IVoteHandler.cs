namespace FastVoteMachine.Services;

public interface IVoteHandler
{
    /// <summary>
    /// Lets a user vote
    /// </summary>
    /// <param name="id">The id of the vote</param>
    /// <param name="option">The option the user wants to vote for.
    /// If the option does not exist, the vote gets ignored</param>
    public void VoteFor(int id, string option);

    /// <summary>
    /// Creates a new Voting 
    /// </summary>
    /// <param name="name">The name of the voting</param>
    /// <returns>A unique id</returns>
    public int CreateVoting(string name);

    /// <summary>
    /// Retrieve information about a vote
    /// </summary>
    /// <param name="id">The id of the vote</param>
    /// <returns>A dictionary with all options and the amount of votes it received</returns>
    public Dictionary<string, int> GetVotes(int id);

    /// <summary>
    /// Get the name of a voting
    /// </summary>
    /// <param name="id">The id of the vote</param>
    /// <returns>Returns the given name of a voting</returns>
    public string GetName(int id);
    
    /// <summary>
    /// Retrieve information about the amount of people connected
    /// </summary>
    /// <param name="id">The id of the vote</param>
    /// <returns>The amount of connections to a given vote</returns>
    public int GetConnected(int id);
    
    /// <summary>
    /// Add a new connection to the vote
    /// </summary>
    /// <param name="id">The id of the vote</param>
    /// <param name="connection">The ConnectionID of the incoming connection</param>
    public void Connect(int id, string connection);

    /// <summary>
    /// Disconnect from a vote
    /// </summary>
    /// <param name="connection">The ConnectionID of the closed connection</param>
    /// <returns>A set of all groups the connection entered</returns>
    public HashSet<int> Disconnect(string connection);
}