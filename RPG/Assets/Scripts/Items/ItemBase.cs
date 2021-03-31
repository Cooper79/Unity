using UnityEngine;
using System;

namespace Items
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] private ItemId _itemId;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private int _stackCount;
        [SerializeField] private Sprite _inventoryIcon;

        public ItemId ItemId => _itemId;
        public string Name => _name;
        public string Description => _description;
        public int StackCount => _stackCount;
        public int Cost => _cost;
        public Sprite InventoryIcon => _inventoryIcon;

    }

    [CreateAssetMenu(fileName = "Consunmable", menuName = "Item/Consunmables")]
    public class ConsumnableBase : ItemBase
    {

    }

    [CreateAssetMenu(fileName = "Readable", menuName = "Item/Readables")]
    public class ReadableBase : ItemBase
    {
        [SerializeField] private string _text;
        public string Text => _text;
    }

    [CreateAssetMenu(fileName = "Potion", menuName = "Item/Potions")]
    public class PotionBase : ItemBase
    {
        [SerializeField] private int _potionLVL;

        public int PorionLVL => _potionLVL;
    }

    public abstract class StatItemBase : ItemBase
    {
        [SerializeField] private int _requiredLVL;
        [SerializeField] private ItemStat[] _primaryStat;

        public int RequiredLVL => _requiredLVL;
        public ItemStat[] PrimaryStat => _primaryStat;
    }


    [CreateAssetMenu(fileName = "EquipmentComponent", menuName = "Item/EquipmentComponents")]
    public class EquipmentComponentBase : StatItemBase
    {
        [SerializeField] private EquipmentType[] _allowedEquipmentTypes;
        [SerializeField] private ComponentType _componentType;

        public EquipmentType[] AllowedEquipmentTypes => _allowedEquipmentTypes;
        public ComponentType ComponentType => _componentType;
    }

    [CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipments")]
    public class EquipmentBase : StatItemBase
    {
        [SerializeField] private Stat[] _requiredStat;
        [SerializeField] private EquipmentType _equipment;
        [SerializeField] private RarityLVL _rarityLVL;
        [SerializeField] private ItemStat[] _additionalStats;

        public Stat[] RequiredStat => _requiredStat;
        public EquipmentType EquipmentType => _equipment;
        public RarityLVL RariryLVL => _rarityLVL;
        public ItemStat[] AdditionalStats => _additionalStats;
    }

    

}

