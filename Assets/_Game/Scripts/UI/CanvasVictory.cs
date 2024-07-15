public class CanvasVictory : UICanvas
{
    public void NextLevelButton()
    {
        LevelManager.instance.DestroyCurrLevel();
        UIManager.instance.CloseAllUI();
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }
}
