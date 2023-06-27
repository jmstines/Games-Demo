using Entities.Enums;
using Entities.HelperFunctions;
using System;
using System.Linq;
using Entities.Interfaces;

namespace Entities.Actions;

public class HitAction: IHitAction
{
	private readonly ICardProvider _cardProvider;

	public HitAction(ICardProvider cardProvider)
	{
		_cardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
	}

	public void PlayerHits(BlackJackGame game, IBlackJackPlayer player, Hand hand)
	{
		hand.AddCard(_cardProvider.Cards(1).Single());

		player.UpdateStatus();

		if (player.Status == PlayerStatusTypes.Complete)
		{
			var playerId = game.Players.First(x => x.Value == player).Key;
			game.UpdateCurrentPlayer(playerId);
		}

		if (game.CurrentPlayer == game.Dealer)
		{
			game.Players[game.CurrentPlayer].DealersTurn(_cardProvider);
		}

		game.SetGameCompleteOnAllPlayersComplete();
	}
}