using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : BaseEnemy
{
    protected override void Death()
    {
        ReturnToPool();
    }
}
