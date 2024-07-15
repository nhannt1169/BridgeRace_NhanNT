public class CanvasLose : UICanvas
{
    public void RestartButton()
    {
        LevelManager.instance.DestroyCurrLevel();
        UIManager.instance.CloseAllUI();
    }
}
