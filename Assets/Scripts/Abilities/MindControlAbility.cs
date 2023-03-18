using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControlAbility : ActiveAbility
{
    [SerializeField] private float _controlDuration;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _targetMask;

    [SerializeField] private BaseCharacter _character;

    private List<BaseEnemy> Enemies;

    private Creep _target;

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

        //Get random creep in radius
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _targetMask);
        Creep[] creepsInRadius = GetCreeps(targetColliders);
        
        if (creepsInRadius.Length == 0) { return; }
        
        _target = GetRandomEnemy(creepsInRadius);

        //Change creep's target (creep's color to yellow)
        Enemies = new List<BaseEnemy>();
        _target.GetUnderControl(_radius, _targetMask, Enemies);//if creep doesnt find target, he will stay afk (for now)
        _target.GetComponent<SpriteRenderer>().color = Color.yellow;//temp

        //When creep hits another enemy, that enemy attacks this creep


        StartCoroutine(ResetEnemy());
    }

    private Creep[] GetCreeps(Collider2D[] enemyColliders)
    {
        List<Creep> creeps = new List<Creep>();
        foreach (var enemy in enemyColliders)
        {
            if (enemy.TryGetComponent(out Creep creep))
            {
                creeps.Add(creep);
            }
        }
        return creeps.ToArray();
    }

    private Creep GetRandomEnemy(Creep[] creeps)
    {
        int randIndex = Random.Range(0, creeps.Length);
        return creeps[randIndex].GetComponent<Creep>();
    }

    private IEnumerator ResetEnemy()
    {
        yield return new WaitForSeconds(_controlDuration);
        //Change creep's target to default
        if (_target.IsUnderControl)
        {
            foreach (var enemy in Enemies)
            {
                enemy.SetTarget(_character.transform);
            }
            _target.GetComponent<SpriteRenderer>().color = Color.red;//temp
        }
    }
}
