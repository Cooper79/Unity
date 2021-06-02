using System;
using Player;
using UnityEngine;

namespace Items
{
    public static class ItemHelper
    {
        public static EquipmentSlotType GetSlotForItem(EquipmentType type)
        {
            switch (type)
            {
                case EquipmentType.Weapon:
                    return EquipmentSlotType.ItemRigth;
                case EquipmentType.Shield:
                    return EquipmentSlotType.ItemLeft;
                default:
                    Debug.LogError("Requested type of " + type + " is not supported yet");
                    return EquipmentSlotType.None;
            }
        }

        public static bool CanBeEquiped(EquipmentType type, EquipmentSlotType slotType)
        {
            switch (type)
            {
                case EquipmentType.Weapon:
                    return slotType == EquipmentSlotType.ItemRigth;
            }
            return false;
        }
    }
    [Serializable]
    public class Stat
    {
        public StatType StatType;
        public int Amount;
    }

    [Serializable]
    public class ItemStat : Stat
    {
        public IncreaserType IncreaserType;
    }
    public enum ItemId
    {
        Gold = 1,
        Scrap = 2,
        Shard = 3,
        HP_Potion = 4,
        MP_Potion = 5,
        Emerald = 6,
        WizardStaff = 7,
    }

    public enum StatType
    {
        Default = 0,
        HP = 1,
        MP = 2,
        Agility = 3,
        Stranght = 4,
        Intelligence = 5,
        Defence = 6,
        Damage = 7,
        AttackSpeed = 8,
    }

    public enum IncreaserType
    {
        Value,
        Percent,
    }

    public enum EquipmentType
    {
        Weapon,
        Shield,
        Helmet,
        Chest,
        Shoulders,
        Leggins,
        Boots,
        Belt,
        Ring,
        Medal,
        Amulet,
        Runa,
    }

    public enum ComponentType
    {
        QuaterComponent,
        HalfComponent,
        FullComponent,
    }

    public enum RarityLVL
    {
        Common = 0,
        Uncommon = 1,
        Magical = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5,
    }
    
}