using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : BaseAbility
{
    [SerializeField] protected float CooldownTime = 1f;

    protected Cooldown AbilityCooldown;

    protected virtual void Awake()
    {
        AbilityCooldown = new Cooldown(CooldownTime, Data.Name);
    }

    public virtual void ActivateAbility() { }
}
