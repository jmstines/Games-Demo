using Entities.Interfaces;

namespace Entities.Providers;

public class GuidBasedPlayerIdentifierProvider : GuidBasedIdentiferProviderBase, IPlayerIdentifierProvider
{
    public string GeneratePlayerId() => Generate(playerIdentifierLength);
}
