using System;
using System.Text.Json;
using System.Collections.Generic;
using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Entities.ResponceDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Entities.RepositoryDto;
using Interactors.Repositories;

namespace BlackJackController.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlackJackGameController : ControllerBase
	{
		private readonly ILogger<BlackJackGameController> _logger;
		private readonly IGameRepository _gameRepo;
		private readonly IHandIdentifierProvider _handIdProvider;
		

		public BlackJackGameController(
			ILogger<BlackJackGameController> logger,
			IGameRepository gameRepository,
			IHandIdentifierProvider handIdProvider
			)
		{
			_logger = logger;
			_gameRepo = gameRepository;
			_handIdProvider = handIdProvider;
		}

		//TODO need to move logic to game class

		//https://localhost:44370/blackJackGame/BeginGame?playerId=%228f4f9270-0f14-45b7-83cd-4262aa8f89d0%22
		[HttpGet]
		[Route("BeginGame")]
		public BlackJackGameModel BeginGame(string gameId, string playerId)
		{
			if (string.IsNullOrEmpty(playerId))
			{
				throw new ArgumentNullException(nameof(playerId));
			}

			if (string.IsNullOrEmpty(gameId))
			{
				throw new ArgumentNullException(nameof(gameId));
			}

			var game = _gameRepo.ReadAsync(gameId);

			game.Players.Single(x => x.Identifier == playerId).Status = PlayerStatusTypes.Ready;

			//TODO: Should Check current player before dealing.
			//TODO: Should Check all players ready.

			game.Deal();
			game.Status = GameStatus.InProgress;
			
			_gameRepo.UpdateAsync(game.Id, game);

			return game.ToModel(game.CurrentPlayer.Identifier);
		}

		[HttpGet]
		[Route("Hit")]
		public BlackJackGameModel Hit(string gameId, string playerId, string  handId)
		{
			//TODO on error return current game with error message
			var game = _gameRepo.ReadAsync(gameId);

			game.PlayerHits(playerId, handId);

			_gameRepo.UpdateAsync(gameId, game);

			return game.ToModel(game.CurrentPlayer.Identifier);
		}

		[HttpGet]
		[Route("Hold")]
		public BlackJackGameModel Hold(string gameId, string playerId, string handId)
		{
			//TODO on error return current game with error message
			var game = _gameRepo.ReadAsync(gameId);

			game.PlayerHolds(playerId, handId);

			_gameRepo.UpdateAsync(gameId, game);

			return game.ToModel(game.CurrentPlayer.Identifier);
		}

		[HttpGet]
		[Route("Split")]
		public BlackJackGameModel Split(string gameId, string playerId, string handId)
		{
			//TODO on error return current game with error message
			var game = _gameRepo.ReadAsync(gameId);

			game.PlayerHolds(playerId, handId);

			_gameRepo.UpdateAsync(gameId, game);

			return game.ToModel(game.CurrentPlayer.Identifier);
		}

		[HttpGet]
		[Route("JoinGame")]
		public BlackJackGameModel JoinGame(string playerId, int numberOfPlayers = 1, int numberOfHands = 1)
		{
			var game = _gameRepo.FindOpenGame(GameStatus.Waiting, numberOfPlayers);

			var avitar = new AvitarDto()
			{
				Id = playerId,
				UserName = "Me",
				EmailAddress = "something@gmail.com"
			};
			var player = new BlackJackPlayer(avitar, _handIdProvider, numberOfHands);

			//TODO: The reay will need to be after some sort of polling
			game.Status = GameStatus.Ready;
			game.AddPlayer(player);

			_gameRepo.UpdateAsync(game.Id, game);

			return game.ToModel(playerId);
		}

		
	}
}
