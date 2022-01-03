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
using Microsoft.AspNetCore.Cors;
using Entities.RepositoryDto;

namespace BlackJackController.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlackJackGameController : ControllerBase
	{
		private readonly ILogger<BlackJackGameController> _logger;
		private readonly ICardProvider _cardProvider;
		private readonly IDealerProvider _dealerProvider;
		private readonly IHandIdentifierProvider _handIdProvider;
		private BlackJackGame CurrentGame;

		public BlackJackGameController(
			ILogger<BlackJackGameController> logger,
			ICardProvider cardProvider,
			IDealerProvider dealerProvider,
			IHandIdentifierProvider handIdProvider
			)
		{
			_logger = logger;
			_cardProvider = cardProvider;
			_dealerProvider = dealerProvider;
			_handIdProvider = handIdProvider;
		}

		//https://localhost:44370/blackJackGame/BeginGame?playerId=%228f4f9270-0f14-45b7-83cd-4262aa8f89d0%22
		[HttpGet]
		[Route("BeginGame")]
		public BlackJackGameDto BeginGame(string playerId, int numberOfPlayers = 1, int numberOfHands = 1)
		{
			if (string.IsNullOrEmpty(playerId))
			{
				throw new ArgumentNullException(nameof(playerId));
			}

			var dealer = _dealerProvider.Dealer;
			var avitar = new AvitarDto()
			{
				Id = playerId,
				UserName = "Me",
				EmailAddress = "something@gmail.com"
			};
			var player = new BlackJackPlayer(avitar, _handIdProvider, numberOfHands);
			
			CurrentGame = new BlackJackGame(_cardProvider, dealer, numberOfPlayers);

			player.Status = PlayerStatusTypes.Ready;
			CurrentGame.AddPlayer(player);
			CurrentGame.DealHands();
			CurrentGame.Status = GameStatus.InProgress;

			return CurrentGame.ToDto(playerId);
		}

		[HttpGet]
		[Route("Hit")]
		public BlackJackGameDto Hit(string playerId, string  handId)
		{
			//TODO implement in memory DB for testing.
			//TODO on error return current game with error message
			CurrentGame.PlayerHits(playerId, handId);

			return CurrentGame.ToDto(playerId);
		}

		//[HttpPost]
		//public string JoinGame()
		//{
		//	var joinGameResponse = GetResponse<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>(
		//		new JoinGameInteractor.RequestModel()
		//	{
		//		PlayerId = id,
		//		MaxPlayers = maxPlayers
		//	});
		//	gameIdentifier = joinGameResponse.GameIdentifier;
		//	return "1234-4567-7890";
		//}

		//private BlackJackGameDto currentGame = new BlackJackGameDto()
		//{
		//	Status = GameStatus.InProgress,
		//	CurrentPlayerId = Guid.NewGuid(),
		//	Players = new List<BlackJackPlayerDto>
		//		{
		//			new BlackJackPlayerDto
		//			{
		//				Name = "Dealer",
		//				PlayerIdentifier = Guid.NewGuid(),
		//				Hands = new List<HandDto>
		//				{
		//					new HandDto
		//					{
		//						Identifier = Guid.NewGuid().ToString(),
		//						Actions = new List<HandActionTypes>
		//						{
		//							HandActionTypes.Hold,
		//							HandActionTypes.Hit,
		//							HandActionTypes.Pass
		//						},
		//						Cards = new List<BlackJackCardDto>
		//						{
		//							new BlackJackCardDto()
		//							{
		//								ImageName = $"card_back_blue"
		//							},
		//							new BlackJackCardDto()
		//							{
		//								Value = 3,
		//								ImageName = $"{CardRank.Three.ToString().ToLower()}_of_{CardSuit.Spades.ToString().ToLower()}"
		//							}
		//						},
		//						CardCount = 2,
		//						PointValue = 3,
		//						Status = HandStatusTypes.InProgress
		//					}
		//				},
		//				Status = PlayerStatusTypes.InProgress
		//			},
		//			new BlackJackPlayerDto
		//			{
		//				Name = "Player 1",
		//				PlayerIdentifier = Guid.NewGuid(),
		//				Hands = new List<HandDto>
		//				{
		//					new HandDto
		//					{
		//						Identifier = Guid.NewGuid().ToString(),
		//						Actions = new List<HandActionTypes>
		//						{
		//							HandActionTypes.Hold,
		//							HandActionTypes.Hit,
		//							HandActionTypes.Pass
		//						},
		//						Cards = new List<BlackJackCardDto>
		//						{
		//							new BlackJackCardDto()
		//							{
		//								Value = 5,
		//								ImageName = $"{CardRank.Five.ToString().ToLower()}_of_{CardSuit.Spades.ToString().ToLower()}"
		//							},
		//							new BlackJackCardDto()
		//							{
		//								Value = 5,
		//								ImageName = $"{CardRank.Five.ToString().ToLower()}_of_{CardSuit.Spades.ToString().ToLower()}"
		//							}
		//						},
		//						CardCount = 2,
		//						PointValue = 10,
		//						Status = HandStatusTypes.InProgress
		//					}
		//				},
		//				Status = PlayerStatusTypes.InProgress
		//			}
		//		}

		//};
	}
}
