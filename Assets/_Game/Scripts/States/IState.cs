public interface IState
{
    void IStart(Bot bot);
    void IUpdate(Bot bot);
    void IStop(Bot bot);
}
