using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using System.Linq;
public class PlayerEquipmentUI : MonoBehaviour
{
    [SerializeField] private GameObject _equipmentHolder;

    public List<EquipmentSlot> EquipmentSlots { get; private set; }

    public void InitComponents()
    {
        EquipmentSlots = _equipmentHolder.GetComponentsInChildren<EquipmentSlot>().ToList();
    }

    public EquipmentSlot GetEquipmentSlotByType(EquipmentSlotType type)
    {
        return EquipmentSlots.First(equip => equip.EquipmentSlotType == type);
    }
}
