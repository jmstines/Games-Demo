﻿using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Interactors.Boundaries;
using Interactors.Repositories;
using System;

namespace Interactors
{
	public class JoinGameInteractor : IInputBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string PlayerId { get; set; }
			public int MaxPlayers { get; set; }
			public int HandCount { get; set; }
		}

		public class ResponseModel
		{
			public string GameIdentifier { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly IGameIdentifierProvider GameIdProviders;
		private readonly IHandIdentifierProvider HandIdProvider;
		private readonly IAvitarRepository AvitarRepository;
		private readonly IDealerProvider DealerProvider;
		private readonly ICardProvider CardProvider;

		public JoinGameInteractor(
			IGameRepository gameRepository, 
			IAvitarRepository avitarRepository, 
			IDealerProvider dealerProvider,
			IGameIdentifierProvider gameIdProviders,
			IHandIdentifierProvider handIdProvider,
			ICardProvider cardProvider
			)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			GameIdProviders = gameIdProviders ?? throw new ArgumentNullException(nameof(gameIdProviders));
			HandIdProvider = handIdProvider ?? throw new ArgumentNullException(nameof(handIdProvider));
			AvitarRepository = avitarRepository ?? throw new ArgumentNullException(nameof(avitarRepository));
			DealerProvider = dealerProvider ?? throw new ArgumentNullException(nameof(dealerProvider));
			CardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel?.PlayerId ?? throw new ArgumentNullException(nameof(requestModel.PlayerId));

			var avitar = AvitarRepository.ReadAsync(requestModel.PlayerId);

			var currentPlayer = new BlackJackPlayer(avitar, HandIdProvider, requestModel.HandCount);

			var keyAndGame = GameRepository.FindByStatusFirstOrDefault(GameStatus.Waiting, requestModel.MaxPlayers);

			string gameIdentifier;
			BlackJackGame game;
			if (string.IsNullOrEmpty(keyAndGame.Key))
			{
				gameIdentifier = GameIdProviders.GenerateGameId();
				game = new BlackJackGame(CardProvider, DealerProvider.Dealer, requestModel.MaxPlayers);
			}
			else
			{
				gameIdentifier = keyAndGame.Key;
				game = keyAndGame.Value;
			}
			
			game.AddPlayer(currentPlayer);

			GameRepository.UpdateAsync(gameIdentifier, game);

			outputBoundary.HandleResponse(new ResponseModel() { GameIdentifier = gameIdentifier });
		}
	}
}
