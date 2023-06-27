using Entities;
using Entities.Enums;
using System.Threading.Tasks;

namespace Interactors.Repositories;

public interface IGameRepository
{
	Task CreateAsync(BlackJackGame game);
	Task<BlackJackGame> ReadAsync(string identifier);
	Task UpdateAsync(string identifier, BlackJackGame game);
	Task<BlackJackGame> FindOpenGame(GameStatus status, int maxPlayers);
}
