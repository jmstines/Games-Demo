using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Entities.RepositoryDto;
using Entities.ResponceDto;
using Interactors.Boundaries;
using Interactors.Repositories;
using System;

namespace Interactors
{
	public class JoinGameInteractor : IInteractorBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string PlayerId { get; set; }
			public int? MaxPlayers { get; set; }
			public int? HandCount { get; set; }
		}

		public class ResponseModel
		{
			public BlackJackGameModel Game { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly IHandIdentifierProvider HandIdProvider;

		public JoinGameInteractor(
			IGameRepository gameRepository, 
			IHandIdentifierProvider handIdProvider
			)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			HandIdProvider = handIdProvider ?? throw new ArgumentNullException(nameof(handIdProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, out ResponseModel responseModel)
		{
			var maxPlayers = MaxPlayersOrDefault(requestModel.HandCount);
			var game = GameRepository.FindOpenGame(GameStatus.Waiting, maxPlayers);

			var avitar = new AvitarDto()
			{
				Id = requestModel.PlayerId,
				UserName = "Me",
				EmailAddress = "something@gmail.com"
			};

			var handCount = HandCountOrDefault(requestModel.HandCount);

			var player = new BlackJackPlayer(avitar, HandIdProvider, handCount);

			//TODO: The reay will need to be after some sort of polling
			game.Status = GameStatus.Ready;
			game.AddPlayer(player);

			GameRepository.UpdateAsync(game.Id, game);

			var gameModel = game.ToModel(game.CurrentPlayer.Identifier);

			responseModel = new ResponseModel() { Game = gameModel };
		}

		private int HandCountOrDefault(int? handCount) => handCount ?? 1;

		private int MaxPlayersOrDefault(int? maxPlayers) => maxPlayers ?? 1;
	}
}
