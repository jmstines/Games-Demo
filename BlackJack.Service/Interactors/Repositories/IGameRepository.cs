using Entities;
using Entities.Enums;

namespace Interactors.Repositories
{
	public interface IGameRepository
	{
		void CreateAsync(BlackJackGame game);
		BlackJackGame ReadAsync(string identifier);
		void UpdateAsync(string identifier, BlackJackGame game);
		BlackJackGame FindOpenGame(GameStatus status, int maxPlayers);
	}
}
