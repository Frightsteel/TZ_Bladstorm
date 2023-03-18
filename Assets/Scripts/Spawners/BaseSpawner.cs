using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] protected MonoBehaviour Prefab;

    [Header("Size Settings")]
    [SerializeField] protected int PoolSize;
    [SerializeField] protected bool AutoExpand;

    public PoolMono<MonoBehaviour> Pool { get; private set; }

    protected virtual void Awake()
    {
        Pool = new PoolMono<MonoBehaviour>(Prefab, PoolSize, transform);
        Pool.autoExpand = AutoExpand;
    }
}
