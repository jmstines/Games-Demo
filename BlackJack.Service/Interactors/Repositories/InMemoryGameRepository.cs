using Entities;
using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interactors.Repositories;

public class InMemoryGameRepo : IGameRepository
{
    private readonly Dictionary<string, BlackJackGame> games = new();

    private readonly IGameIdentifierProvider _gameIdProvider;
    private readonly IDealerProvider _dealerProvider;

    public InMemoryGameRepo(
        IGameIdentifierProvider gameIdProvider,
        IDealerProvider dealerProvider
        )
    {
        _gameIdProvider = gameIdProvider;
        _dealerProvider = dealerProvider;
    }

    public async Task CreateAsync(BlackJackGame game) => 
        await Task.Run(() => games.Add(game.Id, game));

    public async Task<BlackJackGame> FindOpenGame(GameStatus status, int maxPlayers)
    {
        //TODO: This could use some work. aka not efficient
        var gameRecord = games
            .Where(x => x.Value.Status == status 
                && x.Value.Players.Count <= maxPlayers)
            .Select(x => x.Value)
            .FirstOrDefault();

        if (gameRecord != null)
        {
            return gameRecord;
        }
        var gameId = _gameIdProvider.GenerateGameId();
        var dealer = _dealerProvider.Dealer;

        var game = new BlackJackGame()
        {
            Id = gameId,
            MaxPlayerCount = maxPlayers
        };
        await Task.Run(() => games.Add(game.Id, game));

        return game;
    }

    public async Task<BlackJackGame> ReadAsync(string identifier) => 
        await Task.Run(() => games[identifier]);

    public async Task UpdateAsync(string identifier, BlackJackGame game) => 
        await Task.Run(() => games[identifier] = game);
}