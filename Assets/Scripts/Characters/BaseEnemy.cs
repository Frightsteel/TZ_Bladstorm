using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseCharacter, IPoolable
{
    [SerializeField] protected EnemyStats Stats;

    public bool IsUnderControl { get; private set; }

    protected Rigidbody2D Rigidbody;
    protected Collider2D Collider;
    protected Transform DefaultTarget;
    protected Transform Target;
    protected List<BaseEnemy> Enemies;
    protected Cooldown AutoAttackCooldown;

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();

        AutoAttackCooldown = new Cooldown(Stats.DefaultAttackSpeed, name);

        Init();
    }

    protected virtual void Init()
    {
        SetStats();
    }

    protected void SetStats()
    {
        _maxHealth = Stats.DefaultHealth;

        _currentHealth = Stats.DefaultHealth;
        _currentDamage = Stats.DefaultDamage;
        _currentAttackSpeed = Stats.DefaultAttackSpeed;
        _currentMoveSpeed = Stats.DefaultMoveSpeed;
    }

    protected void ResetEnemy()
    {
        SetStats();
        IsUnderControl = false;
        SpriteRenderer.color = DefaultColor;
        //smth else if need (sprite, color and etc)
    }

    protected void FixedUpdate()
    {
        if (Target.gameObject == transform.gameObject)
        {
            //FindTarget();
        }
        Move();
    }

    protected void Move()
    {
        Vector2 direction = (Target.position - transform.position).normalized;
        Rigidbody.velocity = direction * _currentMoveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Target.gameObject.layer && AutoAttackCooldown.IsCooldowned)
        {
            Attack(collision);
            AutoAttackCooldown.StartCooldown();
        }
    }

    public void GetUnderControl(float radius, LayerMask layerMask, List<BaseEnemy> enemies)
    {
        IsUnderControl = true;
        Enemies = enemies;
        Enemies.Add(this);
        FindTarget(radius, layerMask);
        //change color mb;
    }

    private void Attack(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable target))
        {
            target.TakeDamage(_currentDamage);
            Debug.Log($"{collision.gameObject.name} Attacked by {name}");
        }
        if(IsUnderControl && collision.gameObject.TryGetComponent(out BaseEnemy enemy))
        {
            enemy.SetTarget(transform);
            Enemies.Add(enemy);
        }
    }

    public void SetDefaultTarget(Transform target)
    {
        DefaultTarget = target;
        Target = target;
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (Enemies != null)
        {
            foreach (var enemy in Enemies)
            {
                enemy.SetTarget(DefaultTarget);
            }
        }
        ResetEnemy();
    }

    public void FindTarget(float radius, LayerMask targetLayer)
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        if (targetCollider == transform)
        {
            SetTarget(transform);
            return;
        }
        SetTarget(targetCollider.transform);
    }
}
