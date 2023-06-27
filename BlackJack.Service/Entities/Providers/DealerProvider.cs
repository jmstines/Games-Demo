using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Providers
{
    public class DealerProvider : IDealerProvider
	{
		private readonly IAvitarIdentifierProvider AvitarIdentifierProvider;
		private readonly IDictionary<string, IBlackJackPlayer> Dealers;

		public KeyValuePair<string, IBlackJackPlayer> Dealer => RandomDealer();

		public DealerProvider(IAvitarIdentifierProvider avitarIdentifier)
		{
			AvitarIdentifierProvider = avitarIdentifier ?? throw new ArgumentNullException(nameof(avitarIdentifier));
			Dealers = DealersList();
		}

		private KeyValuePair<string, IBlackJackPlayer> RandomDealer()
		{
			var Random = new Random((int)DateTime.UtcNow.Ticks);
			var index = Random.Next(minValue: 0, maxValue: Dealers.Count);
			return Dealers.ElementAt(index);
		}

		private IDictionary<string, IBlackJackPlayer> DealersList()
		{
			var dealers = new Dictionary<string, IBlackJackPlayer>
			{
				{
					AvitarIdentifierProvider.GenerateAvitar(),
					new BlackJackPlayer() {
						Name = "Data"
					}
				},
				{
					AvitarIdentifierProvider.GenerateAvitar(),
					new BlackJackPlayer()
					{
						Name = "Jerry Maguire"
					}
				},
				{
					AvitarIdentifierProvider.GenerateAvitar(),
					new BlackJackPlayer() {
						Name = "James Bond"
					}
				},
				{
					AvitarIdentifierProvider.GenerateAvitar(),
					new BlackJackPlayer() {
						Name = "Rain Man" 
					}
				}
			};

			return dealers;
		}
	}
}