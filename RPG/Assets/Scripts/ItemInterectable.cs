using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInterectable : Interectable
{
    [SerializeField] private ItemBody _thisItem;
    protected override void Interact()
    {
        base.Interact();
        Debug.Log("Take a staff");
        _thisItem.OnPickUp(_player);
    }
}
