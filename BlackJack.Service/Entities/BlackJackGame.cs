using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class BlackJackGame
	{
		private readonly List<BlackJackPlayer> _players = new List<BlackJackPlayer>();
		private readonly ICardProvider _cardProvider;

		public IEnumerable<BlackJackPlayer> Players => _players;
		public BlackJackPlayer CurrentPlayer { get; private set; }
		public BlackJackPlayer Dealer { get; private set; }
		public GameStatus Status { get; set; } = GameStatus.Waiting;
		public int MaxPlayerCount { get; private set; }
		public string Id { get; private set; }

		public BlackJackGame(string gameId, ICardProvider cardProvider, BlackJackPlayer dealer, int maxPlayers)
		{
			Dealer = dealer ?? throw new ArgumentNullException(nameof(dealer));
			if (maxPlayers < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(maxPlayers));
			}

			this._cardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
			Id = gameId;
			MaxPlayerCount = maxPlayers;
		}

		public void AddPlayer(BlackJackPlayer player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (_players.Count >= MaxPlayerCount)
			{
				throw new InvalidOperationException($"{player.Name} can NOT join game, The game Status is {Status}.");
			}
			_players.Add(player);

			SetCurrentPlayerOnFirstPlayerAdd();
			AddDealerToListAfterFinalPlayer();
			SetReadyOnMaxPlayers();
		}

		public void SetPlayerStatusReady(string playerIdentifier)
		{
			var player = GetPlayerById(playerIdentifier);
			if (player == null) 
			{
				throw new ArgumentException(nameof(playerIdentifier), "Player Id NOT Found.");
			}
			player.Status = PlayerStatusTypes.Ready;
			SetGameInProgressOnAllPlayersReady();
		}

		public void PlayerHolds(string playerIdentifier, string handIdentifier)
		{
			_ = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));
			_ = handIdentifier ?? throw new ArgumentNullException(nameof(handIdentifier));
			if (CurrentPlayer.Identifier != playerIdentifier)
			{
				throw new ArgumentException(nameof(playerIdentifier), $"Please wait your turn, Current player is {CurrentPlayer.Name}");
			}

			var player = GetPlayerById(playerIdentifier);
			player.Hold(handIdentifier);

			UpdateCurrentPlayer(player);

			if (CurrentPlayer == Dealer)
			{
				DealersTurn();
			}

			SetGameCompleteOnAllPlayersComplete();
		}

		public void PlayerHits(string playerIdentifier, string handIdentifier)
		{
			_ = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));
			_ = handIdentifier ?? throw new ArgumentNullException(nameof(handIdentifier));
			if (CurrentPlayer.Identifier != playerIdentifier)
			{
				throw new ArgumentException(nameof(playerIdentifier), $"Please wait your turn, Current player is {CurrentPlayer.Name}");
			}

			var player = GetPlayerById(playerIdentifier);
			player.AddCard(handIdentifier, _cardProvider.Cards(1).Single());

			if(player.Status == PlayerStatusTypes.Complete)
			{
				UpdateCurrentPlayer(player);
			}

			if(CurrentPlayer == Dealer)
			{
				DealersTurn();
			}

			SetGameCompleteOnAllPlayersComplete();
		}

		public void Deal()
		{
			if (Status != GameStatus.Ready)
			{
				throw new ArgumentOutOfRangeException(nameof(Status), "Game Status Must be Ready to Deal Hands.");
			}

			var cardCount = _players.Sum(p => p.Hands.Count());
			var cards = _cardProvider.Cards(cardCount);

			_players.ForEach(p => p.DealHands(cards.Take(p.Hands.Count() * 2)));
			
			Status = GameStatus.InProgress;
		}

		private void DealersTurn()
		{
			foreach (var hand in Dealer.Hands)
			{
				while (hand.PointValue < 17 && hand.Cards.Count() <= 5)
				{
					Dealer.AddCard(hand.Identifier, _cardProvider.Cards(1).Single());
				}

				if (hand.Status != HandStatusTypes.Bust)
				{
					Dealer.Hold(hand.Identifier);
				}
			}
		}

		private BlackJackPlayer GetPlayerById(string id)
		{
			return _players.Where(p => p.Identifier == id).SingleOrDefault();
		}

		private void SetCurrentPlayerOnFirstPlayerAdd()
		{
			if (_players.Count == 1)
			{
				CurrentPlayer = _players.First();
			}
		}

		private void UpdateCurrentPlayer(BlackJackPlayer player) => 
			CurrentPlayer = player.Identifier == Dealer.Identifier
					? _players.First()
					: _players.SkipWhile(p => p.Identifier != player.Identifier)
			.Skip(1).Take(1).Single();

		private void AddDealerToListAfterFinalPlayer()
		{
			if (_players.Count == MaxPlayerCount)
			{
				Dealer.Status = PlayerStatusTypes.Ready;
				_players.Add(Dealer);
			}
		}

		private void SetGameCompleteOnAllPlayersComplete() => Status =
				_players.All(p => p.Status.Equals(PlayerStatusTypes.Complete))
				? GameStatus.Complete
				: GameStatus.InProgress;

		private void SetGameInProgressOnAllPlayersReady()
		{
			if (_players.Count >= MaxPlayerCount 
				&& _players.Where(p => p != Dealer).All(p => p.Status.Equals(PlayerStatusTypes.Ready)))
			{
				Status = GameStatus.Ready;
				Dealer.Status = PlayerStatusTypes.Ready;
			}
			else
			{
				Status = GameStatus.Waiting;
			}
		}

		private void SetReadyOnMaxPlayers() => Status = _players.Count - 1 >= MaxPlayerCount
				? GameStatus.Ready
				: GameStatus.Waiting;
	}
}
