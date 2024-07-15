using UnityEngine;

public class BridgingState : IState
{
    public void IStart(Bot bot)
    {
        if (bot.Destination == null)
        {
            bot.SetDestination();
        }
        bot.Moving(bot.Destination.position);
    }

    public void IStop(Bot bot)
    {
        bot.Stopping();
    }

    public void IUpdate(Bot bot)
    {
        //
        if (Vector3.Distance(bot.TF.position, bot.Destination.position) < 0.9f)
        {
            bot.UpdateAtNewStage();
            if (!bot.HasBrick)
            {
                bot.ChangeState(new FindingState());
            }
            else
            {
                bot.ChangeState(new BridgingState());
            }
        }

        if (!bot.HasBrick)
        {
            bot.ChangeState(new FindingState());
        }
    }
}
