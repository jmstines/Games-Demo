using Entities.Interfaces;

namespace Entities.Providers;

public class GuidBasedGameIdentifierProvider : GuidBasedIdentiferProviderBase, IGameIdentifierProvider
{
    public string GenerateGameId() => Generate(gameIdentifierLength);
}
