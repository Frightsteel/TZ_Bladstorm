using System;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, IHealable, IDamagable
{
    #region Stats
    protected float _maxHealth;

    protected float _currentHealth;
    protected float _currentDamage;
    protected float _currentAttackSpeed;
    protected float _currentMoveSpeed;

    public float MaxHealth => _maxHealth;
    public float Health => _currentHealth;
    public float Damage => _currentDamage;
    public float AttackSpeed => _currentAttackSpeed;
    public float MoveSpeed => _currentMoveSpeed;

    #endregion

    #region Modifiers

    private float _maxHpDamage;
    private float _maxHpVampirism;
    
    public float MaxHpDamage
    {
        get { return _maxHpDamage; }
        set
        {
            if (_maxHpDamage + value < 0) _maxHpDamage = 0;
            else _maxHpDamage = value;
        }
    }
    
    public float MaxHpVampirism
    {
        get { return _maxHpVampirism; }
        set
        {
            if (_maxHpVampirism + value < 0) _maxHpVampirism = 0;
            else _maxHpVampirism = value;
        }
    }

    #endregion

    #region Events

    public event Action<float> OnCurrentHealthValueChangedEvent; 

    #endregion

    #region Methods

    public void Heal(float hp)
    {
        if (_currentHealth + hp > MaxHealth)
            _currentHealth = MaxHealth;
        else
            _currentHealth += hp;

        OnCurrentHealthValueChangedEvent?.Invoke(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Death();
        Debug.Log($"{name} - {_currentHealth}");
        OnCurrentHealthValueChangedEvent?.Invoke(_currentHealth);
    }

    protected abstract void Death();

    #endregion
}
