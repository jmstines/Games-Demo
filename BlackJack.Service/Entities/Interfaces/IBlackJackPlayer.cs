using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Interfaces;

public interface IBlackJackPlayer
{
    IEnumerable<string> HandOrder { get; set; }
    IDictionary<string, Hand> Hands { get; set; }
    string Name { get; set; }
    PlayerStatusTypes Status { get; set; }
}