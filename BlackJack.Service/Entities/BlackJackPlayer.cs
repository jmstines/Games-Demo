using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;

namespace Entities;

public class BlackJackPlayer: IBlackJackPlayer
{
    public string Name { get; set; } = string.Empty;
    public IDictionary<string, Hand> Hands { get; set; } = new Dictionary<string, Hand>();
    public IEnumerable<string> HandOrder { get; set; } = new List<string>();
    public PlayerStatusTypes Status { get; set; }
}