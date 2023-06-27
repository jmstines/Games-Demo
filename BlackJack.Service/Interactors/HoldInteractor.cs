using Interactors.Boundaries;
using Interactors.Repositories;
using System;
using Entities;
using Entities.ResponceModel;
using System.Threading.Tasks;
using Entities.Interfaces;
using Entities.Enums;

namespace Interactors;

public class HoldInteractor : IInteractorBoundary<HoldInteractor.RequestModel, HoldInteractor.ResponseModel>
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
    private readonly IHoldAction HoldAction;

    public HoldInteractor(IGameRepository gameRepository, IHoldAction holdAction)
    {
        GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        HoldAction = holdAction ?? throw new ArgumentNullException(nameof(holdAction));
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
            var name = game.Players[game.CurrentPlayer].Name;
            throw new ArgumentException($"Please wait your turn, Current player is {name}", nameof(requestModel.PlayerId));
        }

        if (game.Players.TryGetValue(requestModel.PlayerId, out var player) == false)
        {
            throw new ArgumentException($"Player with id {requestModel.PlayerId} not found.");
        }

        if (player.Hands.TryGetValue(requestModel.HandId, out Hand hand) == false)
        {
            throw new ArgumentException($"{requestModel.HandId} Hand Identifier NOT Found.", nameof(requestModel.HandId));
        }

        if (hand.Status != HandStatusTypes.InProgress)
        {
            throw new InvalidOperationException($"Hand Status Must be In Progress to Hold.");
        }

        HoldAction.PlayerHolds(game, player, hand);

        await GameRepository.UpdateAsync(requestModel.GameId, game);

        var gameModel = game.ToModel(game.CurrentPlayer);

        responseModel = new ResponseModel() { Game = gameModel };
    }
}