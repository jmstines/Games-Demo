using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Entities.ResponceModel;
using Interactors.Boundaries;
using Interactors.Repositories;
using System;
using System.Threading.Tasks;

namespace Interactors;

public class JoinGameInteractor : IInteractorBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>
{
    public record RequestModel
    {
        public string PlayerId { get; set; }
        public int MaxPlayers { get; set; }
        public int HandCount { get; set; }
    }

    public record ResponseModel
    {
        public BlackJackGameModel Game { get; set; }
    }

    private readonly IGameRepository GameRepository;
    private readonly IJoinGameAction JoinGameAction;
    private readonly IAvitarRepository AvitarRepository;

    public JoinGameInteractor(
        IGameRepository gameRepository,
        IJoinGameAction joinGameAction,
        IAvitarRepository avitarRepository)
    {
        GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        JoinGameAction = joinGameAction ?? throw new ArgumentNullException(nameof(joinGameAction));
        AvitarRepository = avitarRepository ?? throw new ArgumentNullException(nameof(avitarRepository));
    }

    public async Task HandleRequestAsync(RequestModel requestModel, ResponseModel responseModel)
    {
        var maxPlayers = MaxPlayersOrDefault(requestModel.HandCount);
        var game = await GameRepository.FindOpenGame(GameStatus.Waiting, maxPlayers);

        // TODO: Make sure the playerid has bee validated.
        // Might have already been validated at the controller
        var player = AvitarRepository.ReadAsync(requestModel.PlayerId);

        if (player is null)
        {
            throw new ArgumentException(nameof(requestModel.PlayerId));
        }

        JoinGameAction.PlayerJoinsGame(game, player.UserName, requestModel.HandCount);

        await GameRepository.UpdateAsync(game.Id, game);

        var gameModel = game.ToModel(game.CurrentPlayer);

        responseModel = new ResponseModel() { Game = gameModel };
    }

    private static int MaxPlayersOrDefault(int? maxPlayers) => maxPlayers ?? 1;
}