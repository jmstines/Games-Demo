using System.Collections.Generic;

namespace Entities.Interfaces;

public interface IDealerProvider
{
    KeyValuePair<string, IBlackJackPlayer> Dealer { get; }
}