using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
public class Player_Inventory
{
    private Player_Creature _player;
    private List<Item2> _inventoryItmes = new List<Item2>();
    private int _inventoryCapacity = 100;

    public Player_Inventory(Player_Creature player)
    {
        _player = player;
    }

    public bool AddItemInventory(Item2 item)
    {
        if (_inventoryItmes.Count < _inventoryCapacity)
        {
            _inventoryItmes.Add(item);
            ShowInventoryItems();
            return true;
        }
        else
            return false;

        
    }

    private void ShowInventoryItems()
    {
        foreach (Item2 inventoryItem in _inventoryItmes)
        {
            Debug.Log(inventoryItem.ItemId);
        }
    }

    public void RemoveItemFromInventory()
    {

    }
}
