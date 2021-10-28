using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJackController.Model;
using Entities;
using Entities.Interfaces;
using Entities.Providers;
using Interactors;
using Interactors.Boundaries;
using Interactors.Providers;
using Interactors.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlackJackController.Controllers
{
	[ApiController]
	[Route("[BlackJackGame]")]
	public class BlackJackGameController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<BlackJackGameController> _logger;

		public BlackJackGameController(ILogger<BlackJackGameController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public string JoinGame()
		{
			var joinGameResponse = GetResponse<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>(
				new JoinGameInteractor.RequestModel()
			{
				PlayerId = id,
				MaxPlayers = maxPlayers
			});
			gameIdentifier = joinGameResponse.GameIdentifier;
			return "1234-4567-7890";
		}

		private TResponseModel GetResponse<TRequestModel, TResponseModel>(TRequestModel requestModel)
		{
			var interactor = C <IInputBoundary<TRequestModel, TResponseModel>>();
			var presenter = new Presenter<TResponseModel>();
			interactor.FromAsync(requestModel, presenter);
			return presenter.ResponseModel;
		}
	}
}
