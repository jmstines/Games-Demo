﻿using Interactors.Boundaries;
using Interactors.Repositories;
using Entities.ResponceDto;
using System;
using Entities;

namespace Interactors
{
	public class HoldInteractor : IInteractorBoundary<HoldInteractor.RequestModel, HoldInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string GameId { get; set; }
			public string PlayerId { get; set; }
			public string HandId { get; set; }
		}

		public class ResponseModel
		{
			public BlackJackGameModel Game { get; set; }
		}

		private readonly IGameRepository GameRepository;

		public HoldInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, out ResponseModel responseModel)
		{
			var game = GameRepository.ReadAsync(requestModel.GameId);

			game.PlayerHolds(requestModel.PlayerId, requestModel.HandId);

			GameRepository.UpdateAsync(requestModel.GameId, game);

			var gameModel = game.ToModel(game.CurrentPlayer.Identifier);

			responseModel = new ResponseModel() { Game = gameModel };
		}
	}
}