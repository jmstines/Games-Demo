using Entities.Enums;
using Entities.ResponceDto;
using System.Collections.Generic;

namespace Entities.ResponceModel;

public record HandModel
{
    public string Identifier { get; set; } = string.Empty;
    public IEnumerable<HandActionTypes>? Actions { get; set; }
    public IEnumerable<BlackJackCardModel>? Cards { get; set; }
    public int CardCount { get; set; } = 0;
    public int? PointValue { get; set; }
    public HandStatusTypes Status { get; set; }
}