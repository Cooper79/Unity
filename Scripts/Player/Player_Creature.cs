using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Creature : Living_Creature   
{
    [SerializeField] private PlayerWindow _playerWindow;

    public Player_Inventory_Controller Player_Inventory_Controller { get; private set; }
    public PlayerEquipmentController PlayerEquipmentController { get; private set; }

    public PlayerInventoryUI PlayerInventoryUI => _playerWindow.PlayerInventoryUI;
    public PlayerEquipmentUI PlayerEquipmentUI => _playerWindow.PlayerEquipmentUI;
    


    public PlayerWindow PlayerWindow => _playerWindow;
    protected override void Start()
    {
        base.Start();
        ActionController = new Player_Action_Controller(this);
        _playerWindow.InitComponents();
        Player_Inventory_Controller = new Player_Inventory_Controller(this);
        PlayerEquipmentController = new PlayerEquipmentController(this);
    }
}
