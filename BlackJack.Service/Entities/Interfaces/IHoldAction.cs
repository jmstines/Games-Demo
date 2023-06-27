namespace Entities.Interfaces;

public interface IHoldAction
{
    public void PlayerHolds(BlackJackGame game, IBlackJackPlayer player, Hand hand);
}
