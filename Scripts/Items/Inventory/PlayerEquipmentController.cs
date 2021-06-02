using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System;
using Player;

public class PlayerEquipmentController
{
    private Player_Creature _player;

    public Dictionary<EquipmentSlotType, Equipment> CurrentEquipment { get; private set; }

    public PlayerEquipmentController(Player_Creature player)
    {
        _player = player;
        CurrentEquipment = new Dictionary<EquipmentSlotType, Equipment>();
        foreach (EquipmentSlotType type in Enum.GetValues(typeof(EquipmentSlotType)))
        {
            if (type == EquipmentSlotType.None)
                continue;
            CurrentEquipment.Add(type, null); 
        }
        foreach(EquipmentSlot slot in _player.PlayerEquipmentUI.EquipmentSlots)
        {
            slot.PlayerCreature = _player;
            slot.LeftPointerClicked += _player.Player_Inventory_Controller.OnMoveStarted;
        }
    }

    public void EquipItem(Equipment equipment)
    {
        if (equipment == null)
            return;

        EquipmentSlotType slotType = GetSlotForItem(equipment.EquipmentBase.EquipmentType);

        if (slotType == EquipmentSlotType.None)
            return;

        TryToRemoveEquipment(slotType);
        EquipItem(slotType, equipment);
        
    }

    public void EquipItem(EquipmentSlotType slotType, Equipment equipment)
    {
        if (slotType == EquipmentSlotType.None || equipment == null)
        {
            return;
        }
        CurrentEquipment[slotType] = equipment;
        EquipmentSlot slot = _player.PlayerEquipmentUI.GetEquipmentSlotByType(slotType);
        slot.RightPointerClicked += RemoveItem;
        slot.AddItemToSlot(equipment);
    }

    public void TryToRemoveEquipment(EquipmentSlotType slotType, bool removetoInventory = true)
    {
        Item2 item = CurrentEquipment[slotType];
        if (item == null)
            return;

        if (removetoInventory)
            _player.Player_Inventory_Controller.AddItemToInventory(item);
        CurrentEquipment[slotType] = null;

        EquipmentSlot slot = _player.PlayerEquipmentUI.GetEquipmentSlotByType(slotType);
        slot.RightPointerClicked -= RemoveItem;
        slot.RemoveItem();
        
        
    }

    private void RemoveItem(InventorySlot slot)
    {
        TryToRemoveEquipment((slot as EquipmentSlot).EquipmentSlotType, true);
    }

    private EquipmentSlotType GetSlotForItem(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                return EquipmentSlotType.ItemRigth;
            case EquipmentType.Shield:
                return EquipmentSlotType.ItemLeft;
            default:
                Debug.LogError("Requested type of " + equipmentType + " is not supported yet");
                return EquipmentSlotType.None;
        }
    }

}
