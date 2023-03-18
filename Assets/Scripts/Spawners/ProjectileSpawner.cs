using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : BaseSpawner
{
    public void Spawn(Transform spawnPoint, Transform targetPoint, float damage, BaseCharacter character)
    {
        Projectile projectile = (Projectile)Pool.GetFreeElement();
        projectile.UpdateStats(spawnPoint, targetPoint, damage, character);
    }
}
