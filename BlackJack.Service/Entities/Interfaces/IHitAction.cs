namespace Entities.Interfaces;

public interface IHitAction
{
    public void PlayerHits(BlackJackGame game, IBlackJackPlayer player, Hand hand);
}
