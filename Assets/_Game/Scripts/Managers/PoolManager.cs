using System;
using UnityEngine;
using static Utils;

public class PoolManager : MonoBehaviour
{
    [SerializeField] PreAllocation[] preAllocations;
    public static PoolManager instance;
    public bool PoolCreated { get; private set; }

    private void Awake()
    {
        instance = this;
        PoolCreated = false;
    }
    public void CreatePools()
    {
        for (int i = 0; i < preAllocations.Length; i++)
        {
            ObjectPool.CreatePool(preAllocations[i].unit, preAllocations[i].amount, preAllocations[i].poolType);
        }
        PoolCreated = true;
    }

    public void DestroyAllPools()
    {
        foreach (PoolType pool in Enum.GetValues(typeof(PoolType)))
        {
            ObjectPool.DestroyPool(pool);
        }
        PoolCreated = false;
    }
}

[System.Serializable]
public class PreAllocation
{
    public GameUnit unit;
    public int amount;
    public PoolType poolType;
}
