using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.HelperFunctions;

public static class PlayerHelpers
{
    public static void UpdateStatus(this IBlackJackPlayer player)
    {
        player.Status = player.Status switch
        {
            PlayerStatusTypes.Waiting => PlayerStatusTypes.Ready,
            PlayerStatusTypes.Ready => PlayerStatusTypes.Ready,
            PlayerStatusTypes.InProgress => CheckEndOfTurn(player),
            PlayerStatusTypes.Complete => PlayerStatusTypes.Complete,
            _ => throw new InvalidOperationException("This status is not supported."),
        };
    }

    public static void AddHands(this IBlackJackPlayer player, IHandIdentifierProvider handIdProvider, int handCount)
    {
        foreach (var id in handIdProvider.GenerateHandIds(handCount))
        {
            player.Hands.Add(id, new Hand());
        }
    }

    private static PlayerStatusTypes CheckEndOfTurn(IBlackJackPlayer player)
    {
        return player.Hands.All(h => h.Value.Status != HandStatusTypes.InProgress)
            ? PlayerStatusTypes.Complete
            : PlayerStatusTypes.InProgress;
    }

    public static void DealersTurn(this IBlackJackPlayer dealer, ICardProvider _cardProvider)
    {
        foreach (var hand in dealer.Hands)
        {
            while (hand.Value.PointValue < 17 && hand.Value.Cards.Count <= BlackJackConstants.MaxHandSize)
            {
                hand.Value.Cards.Add(new BlackJackCard(_cardProvider.Cards(1).Single(), !hand.Value.Cards.Any()));
            }

            if (hand.Value.Status != HandStatusTypes.Bust)
            {
                hand.Value.UpdateState(HandActionTypes.Hold);
            }

            dealer.UpdateStatus();
        }
    }

    public static void AddCardsToHands(this IBlackJackPlayer player, IList<ICard> cards)
    {
        var hands = player.Hands;
        for (int i = 0; i < cards.Count; i++)
        {
            foreach (var hand in hands.Values)
            {
                if (hand.Actions.Contains(HandActionTypes.Hit) == false && hand.Cards.Count > 0)
                {
                    throw new InvalidOperationException("Drawing A card is not a valid Action on this Hand.");
                }

                hand.AddCard(cards[i]);
                i++;
                hand.AddCard(cards[i]);

                hand.UpdateState(HandActionTypes.Hit);
            }
        }
    }
}
