using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.HelperFunctions;

public static class GameHelpers
{
    public static GameStatus GetStatus(this BlackJackGame game)
    {
        var status = game.Status switch
        {
            GameStatus.Waiting => GameStatus.Ready,
            GameStatus.Ready => GameStatus.Ready,
            GameStatus.InProgress => GameStatus.InProgress,
            GameStatus.Complete => GameStatus.Complete,
            _ => throw new InvalidOperationException("This status is not supported."),
        };
        return status;
    }

    public static void AddPlayer(this BlackJackGame game, KeyValuePair<string, IBlackJackPlayer> player)
    {
        game.Players.Add(player);
        game.PlayerOrder.Add(player.Key);
        
        SetCurrentPlayerOnFirstPlayerAdd(game);
    }

    public static void UpdateCurrentPlayer(this BlackJackGame game, string currentPlayer) => 
        game.CurrentPlayer = game.CurrentPlayer == game.Dealer
        ? currentPlayer
        : game.PlayerOrder.SkipWhile(p => p != currentPlayer)
        .Skip(1).Take(1).Single();

    public static void SetGameCompleteOnAllPlayersComplete(this BlackJackGame game) =>
        game.Status = game.Players.All(p => p.Value.Status.Equals(PlayerStatusTypes.Complete))
        ? GameStatus.Complete
        : GameStatus.InProgress;

    private static void SetCurrentPlayerOnFirstPlayerAdd(BlackJackGame game)
    {
        if (game.Players.Count == 1)
        {
            game.CurrentPlayer = game.PlayerOrder.First();
        }
    }
}
