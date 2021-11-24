using System;
using System.Text.Json;
using System.Collections.Generic;
using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Entities.ResponceDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlackJackController.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlackJackGameController : ControllerBase
	{
		private readonly ILogger<BlackJackGameController> _logger;

		public BlackJackGameController(ILogger<BlackJackGameController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public BlackJackGameDto BeginGame()
		{
			return new BlackJackGameDto()
			{
				Status = GameStatus.InProgress,
				CurrentPlayerId = Guid.NewGuid().ToString(),
				Players = new List<BlackJackPlayerDto>
				{
					new BlackJackPlayerDto
					{
						Name = "Dealer",
						PlayerIdentifier = Guid.NewGuid().ToString(),
						Hands = new List<HandDto>
						{
							new HandDto
							{
								Identifier = Guid.NewGuid().ToString(),
								Actions = new List<HandActionTypes>
								{
									HandActionTypes.Hold,
									HandActionTypes.Draw,
									HandActionTypes.Pass
								},
								Cards = new List<BlackJackCardDto>
								{
									new BlackJackCardDto()
									{
										ImageName = $"card_back_blue.jpg"
									},
									new BlackJackCardDto()
									{
										Value = 10,
										ImageName = $"{CardRank.Three}_{CardSuit.Hearts}.png"
									}
								},
								CardCount = 2,
								PointValue = 20,
								Status = HandStatusTypes.InProgress
							}
						},
						Status = PlayerStatusTypes.InProgress
					},
					new BlackJackPlayerDto
					{
						Name = "Player 1",
						PlayerIdentifier = Guid.NewGuid().ToString(),
						Hands = new List<HandDto>
						{
							new HandDto
							{
								Identifier = Guid.NewGuid().ToString(),
								Actions = new List<HandActionTypes>
								{
									HandActionTypes.Hold,
									HandActionTypes.Draw,
									HandActionTypes.Pass
								},
								//Cards = new List<BlackJackCard>
								//{
								//	new BlackJackCard(
								//		new Card (
								//			CardSuit.Hearts,
								//			CardRank.Queen
								//		),
								//		true),
								//	new BlackJackCard(
								//		new Card (
								//			CardSuit.Hearts,
								//			CardRank.Ten
								//		),
								//		false)
								//},
								CardCount = 2,
								PointValue = 20,
								Status = HandStatusTypes.InProgress
							}
						},
						Status = PlayerStatusTypes.InProgress
					}
				}

			};

			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			//return JsonSerializer.Serialize<object>(game, options);
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
	}
}
