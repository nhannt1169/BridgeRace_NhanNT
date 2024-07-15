using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] Player player;
    [SerializeField] Bot[] bots;
    private Dictionary<LevelIdx, Level> levels = new();

    Level currLevel;
    //[SerializeField] NavMeshSurface navMeshSurface;

    private void Awake()
    {
        instance = this;
        Level[] prefabs = Resources.LoadAll<Level>("Level/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            levels.Add((LevelIdx)i, prefabs[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.OpenUI<CanvasMainMenu>();
        //StartLevel(Level.level1);
    }

    public void StartLevel(LevelIdx level)
    {
        //Pool
        if (PoolManager.instance.PoolCreated)
        {
            PoolManager.instance.DestroyAllPools();
        }

        PoolManager.instance.CreatePools();

        //Create level
        currLevel = Instantiate(levels[level]);
        currLevel.SpawnStages();

        //Create player and bots
        player.gameObject.SetActive(true);
        player.TF.position = currLevel.charPositions[0].position;
        int idx = 1;
        foreach (Bot bot in bots)
        {
            bot.TF.position = currLevel.charPositions[idx++].position;
            bot.gameObject.SetActive(true);
            bot.StartNewGame();
        }
    }

    public void DestroyCurrLevel()
    {
        player.ResetStatus();
        player.gameObject.SetActive(false);
        DestroyBots();

        PoolManager.instance.DestroyAllPools();

        currLevel.DestoyCurrLevel();
    }

    public void OnWin(Vector3 position)
    {
        DestroyBots();
        player.Win(position);
    }

    public void DestroyBots()
    {
        foreach (Bot bot in bots)
        {
            bot.ResetStatus();
            bot.gameObject.SetActive(false);
        }
    }

    public void UpdateJoystick(Joystick joystick)
    {
        player.SetJoystick(joystick);
    }
}
