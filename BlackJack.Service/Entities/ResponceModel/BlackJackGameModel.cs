using Entities.Enums;
using System.Collections.Generic;

namespace Entities.ResponceModel;

public class BlackJackGameModel
{
    public List<BlackJackPlayerModel>? Players { get; set; }
    public string? CurrentPlayerId { get; set; }
    public GameStatus Status { get; set; }
    public string? Id { get; init; }
}