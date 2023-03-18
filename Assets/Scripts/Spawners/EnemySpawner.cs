using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    [Header("Custom Settings")]
    [SerializeField] protected Transform Target;
    [SerializeField] private float _startSpawningTime;
    [SerializeField] private float _spawningTimeRate;
    [SerializeField] private float _spawnZoneX;
    [SerializeField] private float _spawnZoneY;

    private float _zoneDelta = 1f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _startSpawningTime, _spawningTimeRate);
    }

    public void Spawn()
    {
        int spawnSide = Random.Range(0, 4);
        Vector3 spawnPoint;

        switch (spawnSide)
        {
            case 0:
                spawnPoint = new Vector3(-_spawnZoneX, Random.Range(-_spawnZoneY, _spawnZoneY), 0f);
                break;

            case 1:
                spawnPoint = new Vector3(_spawnZoneX, Random.Range(-_spawnZoneY, _spawnZoneY), 0f);
                break;

            case 2:
                spawnPoint = new Vector3(Random.Range(-_spawnZoneX, _spawnZoneX), -_spawnZoneY, 0f);
                break;

            case 3:
                spawnPoint = new Vector3(Random.Range(-_spawnZoneX, _spawnZoneX), _spawnZoneY, 0f);
                break;

            default:
                spawnPoint = new Vector3(-_spawnZoneX, Random.Range(-_spawnZoneY, _spawnZoneY), 0f);
                break;
        }

        BaseEnemy enemy = (BaseEnemy)Pool.GetFreeElement();
        enemy.SetDefaultTarget(Target);
        enemy.transform.position = spawnPoint;
    }
}