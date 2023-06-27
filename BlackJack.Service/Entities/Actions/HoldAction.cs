using Entities.Enums;
using Entities.HelperFunctions;
using System;
using Entities.Interfaces;
using System.Linq;

namespace Entities.Actions;

public class HoldAction: IHoldAction
{
	private readonly ICardProvider _cardProvider;

	public HoldAction(ICardProvider cardProvider)
	{
		_cardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));		
	}

	public void PlayerHolds(BlackJackGame game, IBlackJackPlayer player, Hand hand)
	{
        HandHold(player, hand);

		var playerId = game.Players.First(x => x.Value == hand).Key;

		game.UpdateCurrentPlayer(playerId);

		if (game.CurrentPlayer == game.Dealer)
		{
			game.Players[game.CurrentPlayer].DealersTurn(_cardProvider);
		}

		game.SetGameCompleteOnAllPlayersComplete();
	}

	private static void HandHold(IBlackJackPlayer player, Hand hand)
	{
		hand.UpdateState(HandActionTypes.Hold);

		player.UpdateStatus();
	}
}
