using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Stage : GameUnit
{
    public StageIdx stageIdx;
    private int brickAmount;
    [SerializeField] private Transform[] posList;
    public List<Transform> goals;
    public List<Brick> Bricks { get; private set; }
    [SerializeField] bool isFinish;

    public void CreateStage()
    {
        if (isFinish)
        {
            return;
        }
        brickAmount = posList.Length;
        Bricks = new List<Brick>();
        for (int i = 0; i < brickAmount; i++)
        {
            Brick brick = (Brick)ObjectPool.SpawnObject(TF.position, Quaternion.identity, Utils.PoolType.bricks, TF);
            int randomCol = Random.Range(1, BrickColorDict.Count);
            brick.SetColor((ColorType)randomCol);
            brick.TF.position = posList[i].position;

            Bricks.Add(brick);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFinish)
        {
            return;
        }
        if (other.CompareTag(playerTag))
        {
            LevelManager.instance.OnWin(TF.position);
            UIManager.instance.OpenUI<CanvasVictory>();
        }
        else if (other.CompareTag(botTag))
        {
            LevelManager.instance.DestroyBots();
            UIManager.instance.OpenUI<CanvasLose>();
        }

    }
}
