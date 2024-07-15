using UnityEngine;

public class IdleState : IState
{
    float timer;

    public void IStart(Bot bot)
    {
        bot.Stopping();
    }

    public void IStop(Bot bot)
    {
    }

    public void IUpdate(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            if (bot.HasBrick) bot.ChangeState(new BridgingState());
            else bot.ChangeState(new FindingState());
        }
    }
}
