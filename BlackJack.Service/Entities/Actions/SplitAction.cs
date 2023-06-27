using Entities.Enums;
using Entities.Interfaces;
using Entities.HelperFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Actions;

public class SplitAction: ISplitAction
{
	private readonly ICardProvider _cardProvider;
	private readonly IHandIdentifierProvider _handIdProvider;

	public SplitAction(ICardProvider cardProvider, IHandIdentifierProvider handIdProvider)
	{
		_cardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
		_handIdProvider = handIdProvider ?? throw new ArgumentNullException(nameof(handIdProvider));
	}

	public void PlayerSplits(BlackJackGame game, IBlackJackPlayer player, Hand hand)
	{
		PlayerSplit(player.Hands, hand);

		if (player.Status == PlayerStatusTypes.Complete)
		{
			var playerId = game.Players.First(x => x.Value == player).Key;
			game.UpdateCurrentPlayer(playerId);
		}

		if (game.CurrentPlayer == game.Dealer)
		{
			game.Players[game.Dealer].DealersTurn(_cardProvider);
		}

		game.SetGameCompleteOnAllPlayersComplete();
	}

	private void PlayerSplit(IDictionary<string, Hand> hands, Hand hand)
	{
		var ids = _handIdProvider.GenerateHandIds(2);
		foreach(var id in ids)
		{
			hands.Add(id, new Hand());
		}

		var cards = SplitHand(hand);
		var handId = hands.First(x => x.Value == hand).Key;

		hands.Remove(handId);

		foreach(var id in ids)
		{
			hands[id].AddCard((ICard)cards.Take(1));
		}
	}

	public static IEnumerable<IBlackJackCard> SplitHand(Hand hand)
	{
		if (hand.Actions.Contains(HandActionTypes.Split) == false)
		{
			throw new InvalidOperationException("Hand Status is not Eligable for Spliting.");
		}
		var splitCards = new List<IBlackJackCard>();
		hand.Cards.ForEach(c => splitCards.Add(c));
		hand.Cards.Clear();

		return splitCards;
	}
}