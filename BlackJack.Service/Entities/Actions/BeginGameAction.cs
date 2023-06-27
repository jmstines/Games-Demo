using Entities.HelperFunctions;
using System;
using System.Linq;
using Entities.Interfaces;

namespace Entities.Actions;

public class BeginGameAction: IBeginGameAction
{
	private readonly ICardProvider _cardProvider;
	private const int HandMaxCards = 1;

    public BeginGameAction(ICardProvider cardProvider)
	{
		_cardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
	}

	public void PlayerBegins(BlackJackGame game, IBlackJackPlayer player)
	{
		player.UpdateStatus();

		Deal(game);
	}

	private void Deal(BlackJackGame game)
	{
		var cardCount = game.Players.Sum(p => p.Value.Hands.Count) * HandMaxCards;
		var cards = _cardProvider.Cards(cardCount).ToList();

		foreach (var player in game.Players.Values)
		{
			player.AddCardsToHands(cards);
			player.UpdateStatus();
		}
		
		game.GetStatus();
	}
}