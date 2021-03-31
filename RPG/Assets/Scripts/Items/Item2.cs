using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public abstract class Item2
    {
        private ItemBase _itemBase;
        protected Player_Creature _owner;
        public ItemId ItemId => (ItemId)_itemBase.ItemId;
        public int CurrentCost => _itemBase.Cost;
        public Sprite InventoryIcon => _itemBase.InventoryIcon;

        public int CurrentStackCount { get; set; }

        public Item2(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }

        public virtual void Use()
        {



        }

        public void SetOwner(Player_Creature player)
        {
            _owner = player;
        }

        public void ReleaseItem()
        {
            _owner = null;
        }

    }

    public class Readable : Item2
    {
        public ReadableBase ReadableBase { get; private set; }

        public Readable(ReadableBase itemBase) : base(itemBase)
        {
            ReadableBase = itemBase;
        }

        public override void Use()
        {
            base.Use();
            Debug.Log("Reading text");
        }

        
    }

    public class Potion : Item2
    {
        public PotionBase PotionBase { get; private set; }
        public int RestorationAmount => PotionBase.PorionLVL * 250;

        public Potion(PotionBase itemBase) : base(itemBase)
        {
            PotionBase = itemBase;
        }

        public override void Use()
        {
            
            Debug.Log("Restoreed " + RestorationAmount + "stat");
       
        }

    }

    public class Equipment : Item2
    {
        public EquipmentBase EquipmentBase { get; private set; }

        public Equipment(EquipmentBase itemBase) : base(itemBase)
        {
            EquipmentBase = itemBase;
        }

        public override void Use()
        {
            base.Use();
            Debug.Log("Equipment");
        }

    }

    public class EquipmentComponent : Item2
    {
        public EquipmentComponentBase EquipmentBase { get; private set; }

        public EquipmentComponent(EquipmentComponentBase itemBase) : base(itemBase)
        {
            EquipmentBase = itemBase;
        }

        public override void Use()
        {
            base.Use();
            Debug.Log("Start adding component to equipment");
        }

        
    }
}


