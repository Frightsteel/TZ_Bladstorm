using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteAbility : ActiveAbility
{
    [SerializeField] private float _radius;
    [SerializeField] private float _defaultDamage;
    [SerializeField] private float _damagePerTarget;
    [SerializeField] private LayerMask targetMask;

    [SerializeField] private Meteor _meteor;

    private float _multiplier;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivateAbility()
    {
        base.ActivateAbility();

        if (!AbilityCooldown.IsCooldowned)
        {
            Debug.Log("On cooldown");
            return;
        }
        AbilityCooldown.StartCooldown();

        //throw meteor
        _meteor.DropMeteor(GetRandomDropPoint(), _radius, targetMask); 
    }

    private Vector2 GetRandomDropPoint()
    {
        return new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
    }

    private void DealDamage(IDamagable enemy, float damage)
    {
        enemy.TakeDamage(damage);
    }

    private void OnEnable()
    {
        _meteor.OnMeteorFellEvent += OnMerteorFell;
    }

    private void OnMerteorFell(Collider2D[] targetsInRadius)
    {
        //get count of collisions or colliders
        _multiplier = _damagePerTarget * targetsInRadius.Length;

        Debug.Log(targetsInRadius.Length);
        //deal damage
        foreach (var targetCollider in targetsInRadius)
        {
            DealDamage(targetCollider.GetComponent<BaseEnemy>(), _defaultDamage + _multiplier);
        }
        
    }

    private void OnDisable()
    {
        _meteor.OnMeteorFellEvent -= OnMerteorFell;
    }


}
