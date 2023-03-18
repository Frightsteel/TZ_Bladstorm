using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainLightningAbility : ActiveAbility
{
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _targetMask;
    private BaseEnemy[] _targets;

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
        
        List<Collider2D> targetColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _targetMask).ToList();
        _targets = GetRandomEnemies(targetColliders, _jumpCount);

        foreach(var target in _targets)
        {
            DealDamage(target);
        }
    }

    private BaseEnemy[] GetRandomEnemies(List<Collider2D> enemyColliders, int count)
    {
        List<BaseEnemy> enemies = new List<BaseEnemy>();
        if (enemyColliders.Count < count)
            count = enemyColliders.Count;


        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, enemyColliders.Count);
            enemies.Add(enemyColliders[randomIndex].GetComponent<BaseEnemy>());
            enemyColliders.RemoveAt(randomIndex);
        }
        return enemies.ToArray();
    }

    private void DealDamage(BaseEnemy enemy)
    {
        enemy.TakeDamage(_damage);
    }
}
