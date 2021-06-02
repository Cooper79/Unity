using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.EventSystems;

public class Player_Inventory_Controller
{
    private InventorySlot _lastClickedSlot;
    private InventorySlot _newClickedSlot;
    public Item2 MovingItem { get; private set; }

    private Player_Creature _player;
    private List<InventorySlot> _inventoryItemsOcupiedSlots = new List<InventorySlot>();
    private int _InventoryCapacity;

    public Player_Inventory_Controller(Player_Creature player)
    {
        _player = player;
        _player.DestroyHandler += OnDestroy;
        _player.Service_Manager.UpdateHandler += OnUpdate;
        _InventoryCapacity = _player.PlayerInventoryUI.BaseInentroySlots.Length;

        for(int i = 0; i < _InventoryCapacity; i++)
        {
            _player.PlayerInventoryUI.BaseInentroySlots[i].LeftPointerClicked += OnMoveStarted;
            _player.PlayerInventoryUI.BaseInentroySlots[i].PlayerCreature = _player;
        }
        

        
    }

    public bool AddItemToInventory(Item2 item)
    {
        if (_inventoryItemsOcupiedSlots.Count < _InventoryCapacity)
        {
            InventorySlot slot = _player.PlayerInventoryUI.GetFreeSlot();

            if (slot == null)
            {
                return true;
            }

            slot.AddItemToSlot(item);
            slot.RightPointerClicked += OnItemUsed;
            _inventoryItemsOcupiedSlots.Add(slot);
            return true;
        }
        else
            return false;
        
    }
    
    public void OnItemUsed(InventorySlot slot)
    {
        if(slot.SlotItem.Use())
        {
            slot.RightPointerClicked -= OnItemUsed;
            slot.RemoveItem();
            _inventoryItemsOcupiedSlots.Remove(slot);
        }
    }

    private void OnUpdate()
    {
        if (MovingItem != null)
        {
            _player.PlayerInventoryUI.MovingImage.transform.position = Input.mousePosition;

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                EndMove(true);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                EndMove(false);
            }
        }
    }

    private void OnDestroy()
    {
        _player.DestroyHandler -= OnDestroy;
        _player.Service_Manager.UpdateHandler -= OnUpdate;
    }

    public void OnMoveStarted(InventorySlot slot)
    {
        EquipmentSlot equipmentSlot = slot as EquipmentSlot;
        if(equipmentSlot==null || slot.SlotItem != null)
        {
            slot.RightPointerClicked -= OnItemUsed;
        }

        if (_lastClickedSlot != null)
        {
            _newClickedSlot = slot;
            return;
        }

        if(slot.SlotItem != null)
        {
            _lastClickedSlot = slot;
            SetNewMovingItem(slot.SlotItem);

            if (equipmentSlot != null)
            {
                _player.PlayerEquipmentController.TryToRemoveEquipment(equipmentSlot.EquipmentSlotType, false);
            }
            else
            {
                slot.RemoveItem();
            }
        }
    }

    private void EndMove(bool needToMove)
    {
        Item2 newItem = null;
        if (needToMove)
        {
            if (_player.PlayerWindow.PointerOverWindow)
            {
                if (_newClickedSlot != null)
                {
                    EquipmentSlot equipmentSlot = _newClickedSlot as EquipmentSlot;
                    if (equipmentSlot != null)
                    {

                        if (_newClickedSlot.SlotInteractable)
                        {
                            newItem = equipmentSlot.SlotItem;
                            _player.PlayerEquipmentController.EquipItem(equipmentSlot.EquipmentSlotType, MovingItem as Equipment);
                        }
                        else
                            return;
                    }
                    else
                    {
                        newItem = _newClickedSlot.SlotItem;
                        _newClickedSlot.RightPointerClicked += OnItemUsed;
                        _newClickedSlot.AddItemToSlot(MovingItem);
                    }
                }
                else
                    return;

                if (newItem != null)
                {
                    SetNewMovingItem(newItem);
                    return;
                }
            }
            else
            {
                Debug.LogError("drop");
            }
        }
        else
        {
            if(_lastClickedSlot as EquipmentSlot)
            {
                _player.PlayerEquipmentController.EquipItem(MovingItem as Equipment);
            }
            else
            {
                _lastClickedSlot.AddItemToSlot(MovingItem);
                _lastClickedSlot.RightPointerClicked += OnItemUsed;
            }
        }
        _lastClickedSlot = null;
        _newClickedSlot = null;
        MovingItem = null;
        _player.PlayerInventoryUI.MovingImage.color = Color.clear;
        _player.PlayerInventoryUI.MovingImage.sprite = null;
    }

    private void SetNewMovingItem(Item2 item)
    {
        MovingItem = item;
        _player.PlayerInventoryUI.MovingImage.color = Color.white;
        _player.PlayerInventoryUI.MovingImage.sprite = MovingItem.InventoryIcon;
    }
}
