using System.ComponentModel.DataAnnotations.Schema;

namespace FastVoteMachine.Services.Entities;

[Table("votes")]
public class VoteEntity
{
    public int VoteEntityId { get; set; }
    public string Name { get; set; }
    public ICollection<OptionEntity> Options { get; set; }
    public ICollection<ConnectionEntity> Connections { get; set; }
}