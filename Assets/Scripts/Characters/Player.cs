using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerStats _stats;

    public event Action OnDeathEvent;

    protected void Awake()
    {
        SetStats();
    }

    protected void SetStats()
    {
        _maxHealth = _stats.DefaultHealth;

        _currentHealth = _stats.DefaultHealth;
        _currentDamage = _stats.DefaultDamage;
        _currentAttackSpeed = _stats.DefaultAttackSpeed;
    }

    protected override void Death()
    {
        OnDeathEvent?.Invoke();
    }
}
