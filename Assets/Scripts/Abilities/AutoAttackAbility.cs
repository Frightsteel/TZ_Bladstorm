using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackAbility : PassiveAbility
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _targetMask;

    [SerializeField] private ProjectileSpawner _spawner;

    [SerializeField] private BaseCharacter _character;

    private float _damage;

    private void Start()
    {
        _damage = _character.Damage;
        InvokeRepeating(nameof(ActivateAbility), 1f, CooldownTime);
    }

    public void ActivateAbility()
    {
        Shoot(GetClosestEnemy());
    }

    private BaseEnemy GetClosestEnemy()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _targetMask);
        if (enemyColliders.Length != 0)
        {
            if (enemyColliders[0].TryGetComponent(out BaseEnemy enemy))
                return enemy;
        }
        return null;

    }

    private void Shoot(BaseEnemy enemy)
    {
        if (enemy != null)
        {
            _spawner.Spawn(transform, enemy.transform, _damage, _character);
        }
    }

    private void OnTargetHitted(float hp)
    {
        _character.Heal(hp);

    }
}
