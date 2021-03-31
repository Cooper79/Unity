using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCreature : Interectable
{
    protected override void Interact()
    {
        base.Interact();
        Debug.Log("Let`s talk, body!");
        
    }
}
