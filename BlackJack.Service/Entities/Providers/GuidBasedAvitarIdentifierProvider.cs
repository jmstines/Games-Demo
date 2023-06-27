using Entities.Interfaces;

namespace Entities.Providers;

public class GuidBasedAvitarIdentifierProvider : GuidBasedIdentiferProviderBase, IAvitarIdentifierProvider
{
    public string GenerateAvitar() => Generate(avitarIdentifierLength);
}