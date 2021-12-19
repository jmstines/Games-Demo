using System;
using System.Text.Json;
using System.Collections.Generic;
using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Entities.ResponceDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

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
									HandActionTypes.Hit,
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
										Value = 3,
										ImageName = $"{CardRank.Three.ToString().ToLower()}_{CardSuit.Hearts.ToString().ToLower()}.png"
									}
								},
								CardCount = 2,
								PointValue = 3,
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
									HandActionTypes.Hit,
									HandActionTypes.Pass
								},
								Cards = new List<BlackJackCardDto>
								{
									new BlackJackCardDto()
									{
										Value = 5,
										ImageName = $"{CardRank.Five.ToString().ToLower()}_{CardSuit.Clubs.ToString().ToLower()}.png"
									},
									new BlackJackCardDto()
									{
										Value = 5,
										ImageName = $"{CardRank.Five.ToString().ToLower()}_{CardSuit.Hearts.ToString().ToLower()}.png"
									}
								},
								CardCount = 2,
								PointValue = 10,
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
