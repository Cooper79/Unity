using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Item2 _currentItem;
    [SerializeField] private Image _slotImage;

    public void AddItemToSlot(Item2 item)
    {
        _slotImage.sprite = item.InventoryIcon;
        _slotImage.color = Color.white;
        _currentItem = item;
    }

    public void RemoveItem()
    {
        _slotImage.sprite = null;
        _slotImage.color = Color.clear;
        _currentItem = null;
    }

    private void OnLeftClick()
    {
        Debug.Log("LeftClick");
    }

    private void OnRightClick()
    {
        Debug.Log("RightClick");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
}
