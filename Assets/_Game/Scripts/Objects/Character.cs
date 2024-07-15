using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Character : GameUnit
{
    [SerializeField] ColorData colorData;
    public ColorType Color { get; private set; }
    [SerializeField] private SkinnedMeshRenderer mesh;
    protected Stack<CarriedBrick> bricks = new();
    public bool HasBrick => bricks.Count > 0;
    [SerializeField] Animator anim;
    [SerializeField] Utils.PoolType poolType;
    private string currentAnimName;

    public void Awake()
    {
        foreach (var t in BrickColorDict)
        {
            if (t.Value == mesh.material.color)
            {
                ChangeColor(t.Key);
                break;
            }
        }
    }

    public void ChangeColor(ColorType color)
    {
        this.Color = color;
        mesh.material = colorData.GetMat(color);
    }

    public void AddBrick(ColorType color)
    {
        CarriedBrick brick = (CarriedBrick)ObjectPool.SpawnObject(TF.position, Quaternion.identity, poolType, transform);
        brick.SetColor(color);
        float lastHeight;
        if (bricks.Count > 0)
        {

            lastHeight = bricks.Peek().TF.localPosition.y;
        }
        else
        {
            lastHeight = 0;
        }

        brick.TF.localPosition = new Vector3(0, lastHeight + 0.5f, -0.5f);

        bricks.Push(brick);
    }

    public void RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            var brick = bricks.Pop();
            ObjectPool.DespawnObject(brick, poolType);
        }
    }

    public virtual void ResetStatus()
    {
        while (bricks.Count > 0)
        {
            var brick = bricks.Pop();
            ObjectPool.DespawnObject(brick, poolType);
        }
    }

    protected void ChangeAnim(string animName, bool forced = false)
    {
        if (currentAnimName != animName || forced)
        {

            anim.ResetTrigger(currentAnimName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }
}
