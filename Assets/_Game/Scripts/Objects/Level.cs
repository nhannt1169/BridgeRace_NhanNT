using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<Stage> stages;
    public Transform[] charPositions;
    public void SpawnStages()
    {
        Utils.stageDict.Clear();
        for (int i = 0; i < stages.Count; i++)
        {
            stages[i].CreateStage();
            Utils.stageDict.Add(stages[i].stageIdx, stages[i]);
        }
        //navMeshSurface.BuildNavMesh();
    }

    public void DestoyCurrLevel()
    {
        Destroy(gameObject);
    }
}
