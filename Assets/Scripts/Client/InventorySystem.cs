using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> Items;
}

public class Item
{
    public string Name;
    public string Description;
    public ItemType Type;
    public ItemRarity Rarity;
}

public enum ItemType
{
    Misc,
    Material,
    Weapon,
    Equipment
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical,
    Unique
}