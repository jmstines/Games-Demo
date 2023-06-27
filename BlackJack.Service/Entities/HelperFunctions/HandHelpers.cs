using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.HelperFunctions;

public static class HandHelpers
{
    public static void UpdateState(this Hand hand, HandActionTypes action)
    {
        hand.UpdatePointValue();
        hand.UpdateStatus(action);
        hand.UpdateActions();
    }

    public static void AddCard(this Hand hand, ICard card)
    {
        _ = card ?? throw new ArgumentNullException(nameof(card));
        if (hand.Actions.Contains(HandActionTypes.Hit) == false && hand.Cards.Count > 2)
        {
            throw new InvalidOperationException("Drawing A card is not a valid Action on this Hand.");
        }

        hand.Cards.Add(new BlackJackCard(card, !hand.Cards.Any()));
        hand.UpdateState(HandActionTypes.Hit);
    }

    private static void UpdateActions(this Hand hand)
    {
        if (hand.Actions == null)
        {
            hand.Actions = new List<HandActionTypes>();
        }

        if (hand.Cards.Any() != false
            && hand.Cards.Count < BlackJackConstants.MaxHandSize
            && hand.Status != HandStatusTypes.Hold
            && hand.PointValue < BlackJackConstants.BlackJack)
        {
            if (AllowSplit(hand.Cards))
            {
                hand.Actions.Add(HandActionTypes.Split);
            }
            hand.Actions.Add(HandActionTypes.Hit);
            hand.Actions.Add(HandActionTypes.Hold);
        }
    }

    private static void UpdateStatus(this Hand hand, HandActionTypes currentAction)
    {
        hand.Status = currentAction switch
        {
            HandActionTypes.Hit => GetHitStatus(hand),
            HandActionTypes.Hold => HandStatusTypes.Hold,
            HandActionTypes.Split => HandStatusTypes.InProgress,
            _ => throw new InvalidOperationException("This action is not supported."),
        };
    }

    private static int UpdatePointValue(this Hand hand)
    {
        var points = hand.Cards.Sum(card => card.Value);
        var aceCount = hand.Cards.Count(card => card.Rank.Equals(CardRank.Ace));
        for (int i = 0; i < aceCount; i++)
        {
            points = BustHand(hand.PointValue) ? hand.PointValue - BlackJackConstants.DefaultCardValue : hand.PointValue;
        }

        return points;
    }

    private static HandStatusTypes GetHitStatus(in Hand hand)
    {
        HandStatusTypes status;
        if (BustHand(hand.PointValue))
        {
            status = HandStatusTypes.Bust;
        }
        else if (HasMaxCards(hand.Cards.Count) || HitBlackJack(hand.PointValue))
        {
            status = HandStatusTypes.BlackJack;
        }
        else
        {
            status = HandStatusTypes.InProgress;
        }

        return status;
    }

    private static bool AllowSplit(List<BlackJackCard> cards) =>
        cards.Count == 2 && cards.All(c => c.Value == cards.First().Value);

    private static bool BustHand(int pointValue) => pointValue > BlackJackConstants.BlackJack;

    private static bool HitBlackJack(int pointValue) => pointValue == BlackJackConstants.BlackJack;

    private static bool HasMaxCards(int cardCount) => cardCount == BlackJackConstants.MaxHandSize;
}
