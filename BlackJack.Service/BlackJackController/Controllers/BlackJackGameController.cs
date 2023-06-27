using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Interactors;
using Interactors.Boundaries;
using Entities.ResponceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlackJackController.Controllers;

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
	public async Task<IResult> JoinGame(string playerId, int numberOfPlayers, int numberOfHands)
	{
		if (string.IsNullOrWhiteSpace(playerId))
		{
			_logger.LogError($"{nameof(playerId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest();
		}

		var requestModel = new JoinGameInteractor.RequestModel
		{
			PlayerId = playerId,
			MaxPlayers = numberOfPlayers,
			HandCount = numberOfHands
		};

		JoinGameInteractor.ResponseModel response = null;

		await _joinGameInteractor.HandleRequestAsync(requestModel, response);

		if(response.Game == null)
        {
			return Results.NotFound(); 
        }

		return Results.Ok(response.Game);
	}

	//https://localhost:44370/blackJackGame/BeginGame?playerId=%228f4f9270-0f14-45b7-83cd-4262aa8f89d0%22
	[HttpGet]
	[Route("BeginGame")]
	public async Task<IResult> BeginGame(string gameId, string playerId)
	{
		if (string.IsNullOrWhiteSpace(playerId))
		{
			_logger.LogError($"{nameof(playerId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest();
		}

		if (string.IsNullOrWhiteSpace(gameId))
		{
			_logger.LogError($"{nameof(gameId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest();
		}

		var requestModel = new BeginGameInteractor.RequestModel
		{
			GameId = gameId,
			PlayerId = playerId
		};

		BeginGameInteractor.ResponseModel response = null;

		await _beginGameInteractor.HandleRequestAsync(requestModel, response);

		return Results.Ok(response.Game);
	}

	[HttpGet]
	[Route("Hit")]
	public async Task<IResult> Hit(string gameId, string playerId, string  handId)
	{
		if (string.IsNullOrWhiteSpace(handId))
		{
			_logger.LogError($"{nameof(handId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(handId));
		}

		if (string.IsNullOrWhiteSpace(playerId))
		{
			_logger.LogError($"{nameof(playerId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(playerId));
		}

		if (string.IsNullOrWhiteSpace(gameId))
		{
			_logger.LogError($"{nameof(gameId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(gameId));
		}

		var requestModel = new HitInteractor.RequestModel
		{
			GameId = gameId,
			PlayerId = playerId,
			HandId = handId
		};

		HitInteractor.ResponseModel response = null;

		await _hitInteractor.HandleRequestAsync(requestModel, response);

		if(response.Game == null)
        {
			return Results.NotFound();
        }

		return Results.Ok(response.Game);
	}

	[HttpGet]
	[Route("Hold")]
	public async Task<IResult> Hold(string gameId, string playerId, string handId)
	{
		if (string.IsNullOrWhiteSpace(handId))
		{
			_logger.LogError($"{nameof(handId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(handId));
		}

		if (string.IsNullOrWhiteSpace(playerId))
		{
			_logger.LogError($"{nameof(playerId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(playerId));
		}

		if (string.IsNullOrWhiteSpace(gameId))
		{
			_logger.LogError($"{nameof(gameId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(gameId));
		}

		var requestModel = new HoldInteractor.RequestModel
		{
			GameId = gameId,
			PlayerId = playerId,
			HandId = handId
		};

		HoldInteractor.ResponseModel response = null;

		await _holdInteractor.HandleRequestAsync(requestModel, response);

		if(response.Game == null)
        {
			return Results.NotFound();
        }

		return Results.Ok(response.Game);
	}

	[HttpGet]
	[Route("Split")]
	public async Task<IResult> Split(string gameId, string playerId, string handId)
	{
		if (string.IsNullOrWhiteSpace(handId))
		{
			_logger.LogError($"{nameof(handId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(handId));
		}

		if (string.IsNullOrWhiteSpace(playerId))
		{
			_logger.LogError($"{nameof(playerId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(playerId));
		}

		if (string.IsNullOrWhiteSpace(gameId))
		{
			_logger.LogError($"{nameof(gameId)} can not be null.");
			// TODO: Handle Gracefully;
			return Results.BadRequest(nameof(gameId));
		}

		var requestModel = new SplitInteractor.RequestModel
		{
			GameId = gameId,
			PlayerId = playerId,
			HandId = handId
		};

		SplitInteractor.ResponseModel response = null;

		await _splitInteractor.HandleRequestAsync(requestModel, response);

		if(response.Game == null)
        {
			return Results.NotFound();
        }

		return Results.Ok(response.Game);
	}
}