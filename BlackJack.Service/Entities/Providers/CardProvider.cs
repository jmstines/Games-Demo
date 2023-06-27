using Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Entities.Providers;

public class CardProvider : CardProviderBase, ICardProvider
{
	public IEnumerable<ICard> Cards(int count) => RandomCards(count);

	public CardProvider(IRandomProvider randomProvider, Deck deck)
		: base(randomProvider, deck)
	{
		Deck = deck ?? throw new ArgumentNullException(nameof(deck));
		RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
	}
}