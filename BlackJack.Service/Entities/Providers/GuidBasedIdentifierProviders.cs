using Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Entities.Providers;

public class GuidBasedIdentifierProviders : GuidBasedIdentiferProviderBase, IGuidBasedIdentifierProviders
{
    public string GeneratePlayerId() => new GuidBasedPlayerIdentifierProvider().GeneratePlayerId();

    public IEnumerable<string> GenerateHandIds(int count) => new GuidBasedHandIdentifierProvider().GenerateHandIds(count);

    public string GenerateGameId() => new GuidBasedGameIdentifierProvider().GenerateGameId();

    public string GenerateAvitar() => new GuidBasedAvitarIdentifierProvider().GenerateAvitar();
}
