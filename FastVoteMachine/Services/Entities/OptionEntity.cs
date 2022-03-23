using System.ComponentModel.DataAnnotations.Schema;

namespace FastVoteMachine.Services.Entities;

[Table("options")]
public class OptionEntity
{
    public int OptionEntityId { get; set; }
    public int VoteEntityId { get; set; }
    public string Name { get; set; }
    public int Votes { get; set; }
}