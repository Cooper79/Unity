using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Player;
using UnityEngine.EventSystems;

public class EquipmentSlot : InventorySlot, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EquipmentSlotType _slotType;
    public bool SlotInteractable { get; private set; }
    private Color _defaultColor;
    private Sprite _defaultSprite;

    public EquipmentSlotType EquipmentSlotType => _slotType;
    void Start()
    {
        _defaultSprite = _slotImage.sprite;
        _defaultColor = _slotImage.color;
    }

    
    public override void RemoveItem()
    {
        base.RemoveItem();
        _slotImage.color = _defaultColor;
        _slotImage.sprite = _defaultSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item2 item = PlayerCreature.Player_Inventory_Controller.MovingItem;
        if (item != null)
        {
            if (!(item is Equipment) || !ItemHelper.CanBeEquiped((item as Equipment).EquipmentBase.EquipmentType, _slotType))
                SetSlotInteractability(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetSlotInteractability(true);
    }

    private void SetSlotInteractability(bool interactable)
    {
        _slotImage.color = !interactable ? Color.red : IsEquiped ? Color.white : _defaultColor;
        SlotInteractable = interactable;
    }
}
