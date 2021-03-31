using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterecatble : Interectable
{


    protected override void Interact()
    {
        base.Interact();
        Debug.Log("Attack!");
        Destroy(gameObject);
    }
}
