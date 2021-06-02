using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Items;
using System;
using Player;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image _slotImage;


    public bool IsEquiped { get; private set; }
    public Item2 SlotItem { get; private set; }
    public bool SlotInteractable { get; protected set; }
    public Player_Creature PlayerCreature { get; set; }

    public Action<InventorySlot> LeftPointerClicked = delegate { };
    public Action<InventorySlot> RightPointerClicked = delegate { };
    public void AddItemToSlot(Item2 item)
    {
        if (IsEquiped)
            RemoveItem();

        SlotItem = item;
        IsEquiped = true;
        _slotImage.sprite = SlotItem.InventoryIcon;
        _slotImage.color = Color.white;
        
    }

    public virtual void RemoveItem()
    {
        _slotImage.sprite = null;
        _slotImage.color = Color.clear;
        SlotItem = null;
        IsEquiped = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftPointerDown();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (PlayerCreature.Player_Inventory_Controller.MovingItem != null)
                return;
            OnRightPointerDown();
        }
    }

    protected virtual void OnLeftPointerDown()
    {
        LeftPointerClicked(this);
        Debug.Log("LeftClick");
    }

    private void OnRightPointerDown()
    {
        if (!IsEquiped)
            return;

        RightPointerClicked(this);
        Debug.Log("RightClick");
    }

    
}
