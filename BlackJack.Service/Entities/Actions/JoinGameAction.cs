using Entities.Enums;
using Entities.HelperFunctions;
using Entities.Interfaces;
using Entities.RepositoryDto;

namespace Entities.Actions;

public class JoinGameAction: IJoinGameAction
{
	public int MaxPlayerCount { get; private set; } = BlackJackConstants.MaxPlayerCount;

	private readonly IBlackJackPlayerFactory BlackJackPlayerFactory;

    public JoinGameAction(IBlackJackPlayerFactory blackJackPlayerFactory)
    {
        BlackJackPlayerFactory = blackJackPlayerFactory;
    }

    public void PlayerJoinsGame(BlackJackGame game, string playerName, int? handCount)
	{
		var player = BlackJackPlayerFactory.Create(PlayerTypes.Player, handCount);
		player.Value.Name = playerName;

		game.Status = GameStatus.Ready;
		game.AddPlayer(player);

		AddDealerToListAfterFinalPlayer(game);
		SetReadyOnMaxPlayers(game);
	}

	private void AddDealerToListAfterFinalPlayer(BlackJackGame game)
	{
		if (game.Players.Count == BlackJackConstants.MaxPlayerCount)
		{
            var dealer = BlackJackPlayerFactory.Create(PlayerTypes.Dealer);
			
			game.AddPlayer(dealer);
		}
	}

	private void SetReadyOnMaxPlayers(BlackJackGame game) => game.Status = game?.Players.Count - 1 >= MaxPlayerCount
		? GameStatus.Ready
		: GameStatus.Waiting;
}