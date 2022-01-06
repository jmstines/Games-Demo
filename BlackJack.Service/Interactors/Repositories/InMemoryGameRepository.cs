using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Interactors.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackController.Data
{
	public class InMemoryGameRepo : IGameRepository
	{
		private readonly Dictionary<string, BlackJackGame> games = new Dictionary<string, BlackJackGame>();

		private readonly IGameIdentifierProvider _gameIdProvider;
		private readonly ICardProvider _cardProvider;
		private readonly IDealerProvider _dealerProvider;

		public InMemoryGameRepo(
			IGameIdentifierProvider gameIdProvider,
			ICardProvider cardProvider,
			IDealerProvider dealerProvider
			)
		{
			_gameIdProvider = gameIdProvider;
			_cardProvider = cardProvider;
			_dealerProvider = dealerProvider;
		}

		public void CreateAsync(BlackJackGame game)
		{
			games.Add(game.Id, game);
		}

		public BlackJackGame FindOpenGame(GameStatus status, int maxPlayers)
		{
			//TODO: This could use some work. aka not efficient
			var gameRecord = games
				.Where(x => x.Value.Status == status && x.Value.Players.Count() <= maxPlayers)
				.Select(x => x.Value)
				.FirstOrDefault();

			if (gameRecord != null)
			{
				return gameRecord;
			}
			var gameId = _gameIdProvider.GenerateGameId();
			var dealer = _dealerProvider.Dealer;

			var game = new BlackJackGame(gameId, _cardProvider, dealer, maxPlayers);
			games.Add(game.Id, game);

			return game;
		}

		public BlackJackGame ReadAsync(string identifier)
		{
			return games[identifier];
		}

		public void UpdateAsync(string identifier, BlackJackGame game)
		{
			games[identifier] = game;
		}
	}
}
