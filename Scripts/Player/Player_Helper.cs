using System;

namespace Player
{
    [Serializable]
    public class Stat
    {
        public StatType StatTyppe;
        public int Amount;
    }

    public enum StatType
    {
        Default = 0,
        HP = 1,
        MP = 2,
        Agility = 3,
        Strengh = 4,
        Intelligence = 5,
        Defence = 6,
        Damage = 7,
        AtackSpeed = 8,
    }

    public enum EquipmentSlotType
    {
        None,
        ItemLeft,
        ItemRigth,
        Helmet,
        Chest,
        Shoulders,
        Gloves,
        Leggins,
        Boots,
        Belt,
        RingLeft,
        RingRight,
        Medal,
        Amulet,
        Rune,
    }
}
