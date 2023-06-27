using Entities.Enums;
using Entities.HelperFunctions;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Entities.Factorys;

public class BlackJackPlayerFactory: IBlackJackPlayerFactory
{
    private readonly IHandIdentifierProvider HandIdProvider;
    private readonly IDealerProvider DealerProvider;
    private readonly IPlayerIdentifierProvider PlayerIdProvider;

    public BlackJackPlayerFactory(IHandIdentifierProvider handIdProvider, IDealerProvider dealerProvider, IPlayerIdentifierProvider playerIdProvider)
    {
        HandIdProvider = handIdProvider;
        DealerProvider = dealerProvider;
        PlayerIdProvider = playerIdProvider;
    }

    public KeyValuePair<string, IBlackJackPlayer> Create(PlayerTypes type, int? handCount = 1)
    {
        return type switch
        {
            PlayerTypes.Player => GetPlayer(HandCountOrDefault(handCount)),
            PlayerTypes.Dealer => GetDealer(),
            PlayerTypes.Ai => throw new NotImplementedException(nameof(PlayerTypes.Ai)),
            _ => throw new InvalidEnumArgumentException(),
        };
    }

    private KeyValuePair<string, IBlackJackPlayer> GetPlayer(int handCount)
    {
        var player = new BlackJackPlayer()
        {
            Status = PlayerStatusTypes.Waiting,
        };

        player.AddHands(HandIdProvider, HandCountOrDefault(handCount));
        player.HandOrder = player.Hands.Keys.ToList();

        return new KeyValuePair<string, IBlackJackPlayer> (PlayerIdProvider.GeneratePlayerId(), player);
    }

    private KeyValuePair<string, IBlackJackPlayer> GetDealer()
    {
        var dealer = DealerProvider.Dealer;

        dealer.Value.AddHands(HandIdProvider, 1);
        dealer.Value.HandOrder = dealer.Value.Hands.Keys.ToList();
        dealer.Value.Status = PlayerStatusTypes.Ready;

        return dealer;
    }

    private static int HandCountOrDefault(int? handCount) => handCount ?? 1;
}