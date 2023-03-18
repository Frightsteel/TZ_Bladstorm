using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _speed = 15f;
    private float _damage;
    private BaseCharacter _masterCharacter;
    private float _timeToHide = 8f;

    private Vector3 _moveDirection;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), _timeToHide);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveDirection * _speed;
    }

    public void UpdateStats(Transform spawnPoint, Transform targetPoint, float damage, BaseCharacter character)
    {
        _damage = damage;
        _masterCharacter = character;

        transform.position = spawnPoint.position;
        _moveDirection = (targetPoint.position - transform.position).normalized;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(_moveDirection));
    }

    private float GetAngleFromVector(Vector3 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BaseEnemy enemy))
        {
            enemy.TakeDamage(_damage + enemy.MaxHealth * _masterCharacter.MaxHpDamage);
            _masterCharacter.Heal(enemy.MaxHealth * _masterCharacter.MaxHpVampirism);
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}