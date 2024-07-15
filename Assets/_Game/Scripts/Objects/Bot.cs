using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Utils;

public class Bot : Character
{
    private StageIdx stage;
    [SerializeField] private NavMeshAgent agent;
    public Transform TargetBrick { private set; get; }
    public Transform Destination { private set; get; }
    private IState currentState;
    public bool IsMoving => !agent.isStopped;//TODO : fix
    private List<Brick> possibleBricks = new();

    // Start is called before the first frame update
    void Start()
    {
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.IUpdate(this);
    }

    public void Moving(Vector3 position)
    {
        ChangeAnim(animMove, true);
        agent.SetDestination(position);
        agent.isStopped = false;
    }

    public void Stopping()
    {
        ChangeAnim(animIdle);
        agent.SetDestination(TF.position);
    }

    public void ChangeState(IState newState)
    {
        currentState?.IStop(this);

        currentState = newState;

        currentState?.IStart(this);
    }

    public void SetTargetBrick()
    {
        Stage currStage = stageDict[stage];
        if (possibleBricks.Count == 0)
        {
            possibleBricks = new List<Brick>();
            foreach (Brick brick in currStage.Bricks)
            {
                if (brick.Color == Color)
                {
                    possibleBricks.Add(brick);
                }
            }
        }
        if (possibleBricks.Count == 0)
        {
            Debug.LogError("Error:" + Color);
        }
        TargetBrick = possibleBricks[Random.Range(0, possibleBricks.Count)].TF;
    }

    public void SetDestination()
    {
        Stage currStage = stageDict[stage];
        Destination = currStage.goals[Random.Range(0, currStage.goals.Count)];
        if (currStage.goals.Count > 1)
        {
            currStage.goals.Remove(Destination);
        }
    }

    public void UpdateAtNewStage()
    {
        Destination = null;
        TargetBrick = null;
        possibleBricks.Clear();
        stage++;
        SetDestination();
    }

    public override void ResetStatus()
    {
        base.ResetStatus();
        //agent.SetDestination(Vector3.zero);
        TargetBrick = null;
        Destination = null;
        stage = StageIdx.first;
        possibleBricks.Clear();
    }

    public void StartNewGame()
    {
        stage = StageIdx.first;
        ChangeState(new IdleState());
    }
}
