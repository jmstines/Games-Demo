using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;

namespace Entities;

public class BlackJackGame
{
	public IDictionary<string, IBlackJackPlayer> Players { get; } = new Dictionary<string, IBlackJackPlayer>();
	public IList<string> PlayerOrder = new List<string>();
    public string CurrentPlayer { get; set; } = string.Empty;
	public string? Dealer { get; set; }
	public GameStatus Status { get; set; } = GameStatus.Waiting;
	public int MaxPlayerCount { get; set; } = 1;
	public string Id { get; init; } = string.Empty;
}