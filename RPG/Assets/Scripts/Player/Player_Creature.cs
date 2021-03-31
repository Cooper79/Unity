using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Creature : Living_Creature   
{

    public Player_Inventory Player_Inventory { get; private set; }
    protected override void Start()
    {
        base.Start();
        ActionController = new Player_Action_Controller(this);
        Player_Inventory = new Player_Inventory(this);
    }
}
