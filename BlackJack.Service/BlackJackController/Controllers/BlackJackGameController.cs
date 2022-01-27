using Entities.ResponceDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Interactors;
using Interactors.Boundaries;

namespace BlackJackController.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlackJackGameController : ControllerBase
	{
		private readonly ILogger<BlackJackGameController> _logger;

		private readonly IInteractorBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel> _joinGameInteractor;
		private readonly IInteractorBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel> _beginGameInteractor;
		private readonly IInteractorBoundary<HitInteractor.RequestModel, HitInteractor.ResponseModel> _hitInteractor;
		private readonly IInteractorBoundary<HoldInteractor.RequestModel, HoldInteractor.ResponseModel> _holdInteractor;
		private readonly IInteractorBoundary<SplitInteractor.RequestModel, SplitInteractor.ResponseModel> _splitInteractor;

		public BlackJackGameController(
			ILogger<BlackJackGameController> logger,
			IInteractorBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel> joinGameInteractor,
			IInteractorBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel> beginGameInteractor,
			IInteractorBoundary<HitInteractor.RequestModel, HitInteractor.ResponseModel> hitInteractor,
			IInteractorBoundary<HoldInteractor.RequestModel, HoldInteractor.ResponseModel> holdInteractor,
			IInteractorBoundary<SplitInteractor.RequestModel, SplitInteractor.ResponseModel> splitInteractor
			)
		{
			_logger = logger;
			_joinGameInteractor = joinGameInteractor;
			_beginGameInteractor = beginGameInteractor;
			_hitInteractor = hitInteractor;
			_holdInteractor = holdInteractor;
			_splitInteractor = splitInteractor;
		}		

		[HttpGet]
		[Route("JoinGame")]
		public BlackJackGameModel JoinGame(string playerId, int? numberOfPlayers, int? numberOfHands)
		{
			var requestModel = new JoinGameInteractor.RequestModel
			{
				PlayerId = playerId,
				MaxPlayers = numberOfPlayers,
				HandCount = numberOfHands
			};

			_joinGameInteractor.HandleRequestAsync(requestModel, out JoinGameInteractor.ResponseModel response);

			return response.Game;
		}

		//https://localhost:44370/blackJackGame/BeginGame?playerId=%228f4f9270-0f14-45b7-83cd-4262aa8f89d0%22
		[HttpGet]
		[Route("BeginGame")]
		public BlackJackGameModel BeginGame(string gameId, string playerId)
		{
			if (string.IsNullOrEmpty(playerId))
			{
				_logger.LogError($"{nameof(playerId)} can not be null.");
				// TODO: Handle Gracefully;
				return null;
			}

			if (string.IsNullOrEmpty(gameId))
			{
				_logger.LogError($"{nameof(gameId)} can not be null.");
				// TODO: Handle Gracefully;
				return null;
			}

			var requestModel = new BeginGameInteractor.RequestModel
			{
				GameId = gameId,
				PlayerId = playerId
			};

			_beginGameInteractor.HandleRequestAsync(requestModel, out BeginGameInteractor.ResponseModel response);

			return response.Game;
		}

		[HttpGet]
		[Route("Hit")]
		public BlackJackGameModel Hit(string gameId, string playerId, string  handId)
		{
			var requestModel = new HitInteractor.RequestModel
			{
				GameId = gameId,
				PlayerId = playerId,
				HandId = handId
			};

			_hitInteractor.HandleRequestAsync(requestModel, out HitInteractor.ResponseModel response);

			return response.Game;
		}

		[HttpGet]
		[Route("Hold")]
		public BlackJackGameModel Hold(string gameId, string playerId, string handId)
		{
			var requestModel = new HoldInteractor.RequestModel
			{
				GameId = gameId,
				PlayerId = playerId,
				HandId = handId
			};

			_holdInteractor.HandleRequestAsync(requestModel, out HoldInteractor.ResponseModel response);

			return response.Game;
		}

		[HttpGet]
		[Route("Split")]
		public BlackJackGameModel Split(string gameId, string playerId, string handId)
		{
			var requestModel = new SplitInteractor.RequestModel
			{
				GameId = gameId,
				PlayerId = playerId,
				HandId = handId
			};

			_splitInteractor.HandleRequestAsync(requestModel, out SplitInteractor.ResponseModel response);

			return response.Game;
		}
	}
}
