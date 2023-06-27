using Interactors.Boundaries;
using Interactors.Repositories;
using System;
using Entities;
using Entities.ResponceModel;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace Interactors;

public class HitInteractor : IInteractorBoundary<HitInteractor.RequestModel, HitInteractor.ResponseModel>
{
    public record RequestModel
    {
        public string GameId { get; set; }
        public string PlayerId { get; set; }
        public string HandId { get; set; }
    }

    public record ResponseModel
    {
        public BlackJackGameModel Game { get; set; }
    }

    private readonly IGameRepository GameRepository;
    private readonly IHitAction HitAction;

    public HitInteractor(IGameRepository gameRepository, IHitAction hitAction)
    {
        GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        HitAction = hitAction ?? throw new ArgumentNullException(nameof(hitAction));
    }

    public async Task HandleRequestAsync(RequestModel requestModel, ResponseModel responseModel)
    {
        var game = await GameRepository.ReadAsync(requestModel.GameId);

        if (game == null)
        {
            return;
        }

        if (game.CurrentPlayer != requestModel.PlayerId)
        {
            var name = game.Players[requestModel.PlayerId].Name;
            throw new ArgumentException(nameof(requestModel.PlayerId), $"Please wait your turn, Current player is {name}");
        }

        if (game.Players.TryGetValue(requestModel.PlayerId, out var player) == false)
        {
            throw new ArgumentException($"Player with id {requestModel.PlayerId} not found.");
        }

        if (player.Hands.TryGetValue(requestModel.HandId, out var hand) == false)
        {
            throw new ArgumentException($"Hand with id {requestModel.HandId} not found.");
        }

        HitAction.PlayerHits(game, player, hand);

        await GameRepository.UpdateAsync(requestModel.GameId, game);

        var gameModel = game.ToModel(game.CurrentPlayer);

        responseModel = new ResponseModel() { Game = gameModel };
    }
}