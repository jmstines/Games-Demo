using Entities.Enums;
using System.Collections.Generic;

namespace Entities.ResponceModel;

public record BlackJackPlayerModel
{
    public string? Name { get; set; }
    public string? Id { get; set; }
    public List<HandModel>? Hands { get; set; }
    public PlayerStatusTypes Status { get; set; }
}