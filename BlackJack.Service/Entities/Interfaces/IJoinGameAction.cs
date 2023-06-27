namespace Entities.Interfaces;

public interface IJoinGameAction
{
    public void PlayerJoinsGame(BlackJackGame game, string playerId, int? handCount);
}
