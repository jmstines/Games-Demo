using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Providers;

abstract public class CardProviderBase
{
    protected CardProviderBase(IRandomProvider randomProvider, Deck deck)
    {
        Deck = deck ?? throw new ArgumentNullException(nameof(deck));
        RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
    }

    public virtual IEnumerable<ICard> Deck { get; init; }
    public virtual IRandomProvider RandomProvider { get; init; }
    protected virtual IEnumerable<ICard> RandomCards(int count)
    {
        var source = new List<ICard>(Deck);
        var shuffled = new List<ICard>();
        for (int i = 0; i < count; i++)
        {
            var nextIndex = RandomProvider.GetRandom(min: 0, max: source.Count);
            var currentItem = source.ElementAt(nextIndex);
            source.Remove(currentItem);
            shuffled.Add(currentItem);
        }
        return shuffled;
    }
}