using Entities;
using Entities.Enums;
using Interactors.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJackController.Data
{
	public class InMemoryGameRepo: IGameRepository
	{
		private readonly Dictionary<string, BlackJackGame> games = new Dictionary<string, BlackJackGame>();

		public void CreateAsync(BlackJackGame game)
		{
			games.Add(game.Id, game);
		}

		public KeyValuePair<string, BlackJackGame> FindByStatusFirstOrDefault(GameStatus status, int maxPlayers)
		{
			throw new NotImplementedException();
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
