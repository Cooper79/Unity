using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemySlymeController : EnemyControllerBase
{
    public override void TakeDamage(int damage, DamageType type = DamageType.Casual, Transform player = null)
    {
        if (type != DamageType.Projectile)
            return;

        base.TakeDamage(damage, type, player);
    }
    
    public void OffHurt()
    {
        _enemyAnimator.SetBool(EnemyState.Hurt.ToString(), false);
    }
}
