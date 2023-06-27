using Interactors.Boundaries;
using Interactors.Repositories;
using System;
using Entities;
using Entities.ResponceModel;
using System.Threading.Tasks;
using Entities.Interfaces;
using Entities.Enums;

namespace Interactors;

public class BeginGameInteractor : IInteractorBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>
{
    public record RequestModel
    {
        public string GameId { get; set; }
        public string PlayerId { get; set; }
    }

    public record ResponseModel
    {
        public BlackJackGameModel Game { get; set; }
    }

    private readonly IGameRepository GameRepository;
    private readonly IBeginGameAction BeginGameAction;

    public BeginGameInteractor(IGameRepository gameRepository, IBeginGameAction beginGameAction)
    {
        GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        BeginGameAction = beginGameAction ?? throw new ArgumentNullException(nameof(beginGameAction));
    }

    public async Task HandleRequestAsync(RequestModel requestModel, ResponseModel responseModel)
    {
        var game = await GameRepository.ReadAsync(requestModel.GameId);

        if (game == null)
        {
            return;
        }

        if (game.Players.TryGetValue(requestModel.PlayerId, out var player) == false)
        {
            throw new ArgumentException($"Player with id {requestModel.PlayerId} not found.");
        }

        if (game?.Status != GameStatus.Ready)
        {
            throw new ArgumentOutOfRangeException(nameof(game.Status), "Game Status Must be Ready to Deal Hands.");
        }

        BeginGameAction.PlayerBegins(game, player);

        //TODO: Should Check current player before dealing.
        //TODO: Should Check all players ready.

        await GameRepository.UpdateAsync(game.Id, game);

        var gameModel = game.ToModel(game.CurrentPlayer);

        responseModel = new ResponseModel() { Game = gameModel };
    }
}