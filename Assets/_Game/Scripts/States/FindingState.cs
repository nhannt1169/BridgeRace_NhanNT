using UnityEngine;

public class FindingState : IState
{
    public void IStart(Bot bot)
    {
        bot.SetTargetBrick();

        bot.Moving(bot.TargetBrick.position);
    }

    public void IStop(Bot bot)
    {
    }

    public void IUpdate(Bot bot)
    {
        if (bot.TargetBrick == null)
        {
            bot.SetTargetBrick();
        }
        if (!bot.IsMoving)
        {
            bot.Moving(bot.TargetBrick.position);
        }

        //Vector3 point = bot.GetTargetBrick();

        if (Vector3.Distance(bot.TF.position, bot.TargetBrick.position) < 0.9f)
        {
            if (bot.HasBrick)
            {

                bot.ChangeState(new BridgingState());
            }
        }
    }
}
