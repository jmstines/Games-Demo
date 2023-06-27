using Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Entities.Providers;

public class HandProvider : CardProviderBase, IHandProvider
{
	public IDictionary<string, Hand> Hands(IEnumerable<string> identifiers)
	{
		var hands = new Dictionary<string, Hand>();
		foreach(var id in identifiers)
		{
			hands.Add(id, new Hand());
		}
		return hands;
	}

	public HandProvider(IRandomProvider randomProvider, Deck deck)
		: base(randomProvider, deck)
	{
		Deck = deck ?? throw new ArgumentNullException(nameof(deck));
		RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
	}
}