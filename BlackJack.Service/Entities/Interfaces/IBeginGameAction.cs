namespace Entities.Interfaces;

public interface IBeginGameAction
{
    public void PlayerBegins(BlackJackGame game, IBlackJackPlayer player);
}
