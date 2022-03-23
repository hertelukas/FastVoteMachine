using System.ComponentModel.DataAnnotations.Schema;

namespace FastVoteMachine.Services.Entities;

[Table("connections")]
public class ConnectionEntity
{
    public int ConnectionEntityId { get; set; }
    public string Connection { get; set; }
    public ICollection<VoteEntity> Votes { get; set; }
}