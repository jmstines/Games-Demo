﻿using Interactors.Boundaries;
using Interactors.Repositories;
using Entities.ResponceDto;
using System;
using Entities;
using Entities.Enums;
using System.Linq;

namespace Interactors
{
	public class BeginGameInteractor : IInteractorBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string GameId { get; set; }
			public string PlayerId { get; set; }
		}

		public class ResponseModel
		{
			public BlackJackGameModel Game { get; set; }
		}

		private readonly IGameRepository GameRepository;

		public BeginGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, out ResponseModel responseModel)
		{
			if (string.IsNullOrEmpty(requestModel.PlayerId))
			{
				throw new ArgumentNullException(nameof(requestModel.PlayerId));
			}

			if (string.IsNullOrEmpty(requestModel.GameId))
			{
				throw new ArgumentNullException(nameof(requestModel.GameId));
			}

			var game = GameRepository.ReadAsync(requestModel.GameId);

			game.Players.Single(x => x.Identifier == requestModel.PlayerId).Status = PlayerStatusTypes.Ready;

			//TODO: Should Check current player before dealing.
			//TODO: Should Check all players ready.

			game.Deal();
			game.Status = GameStatus.InProgress;

			GameRepository.UpdateAsync(game.Id, game);

			var gameModel = game.ToModel(game.CurrentPlayer.Identifier);

			responseModel = new ResponseModel() { Game = gameModel };
		}
	}
}