namespace Entities.Interfaces;

public interface ISplitAction
{
    public void PlayerSplits(BlackJackGame game, IBlackJackPlayer player, Hand hand);
}
