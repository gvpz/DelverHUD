using System.Collections.Generic;
using UnityEngine;

public class LootboxSystem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;

    [Header("Rarity Weights")]
    public Dictionary<ItemRarity, float> rarityWeights = new()
    {
        { ItemRarity.Common, 50 },
        { ItemRarity.Uncommon, 25 },
        { ItemRarity.Rare, 12 },
        { ItemRarity.Epic, 7 },
        { ItemRarity.Legendary, 4 },
        { ItemRarity.Mythical, 1.5f },
        { ItemRarity.Unique, 0.5f }
    };

    public Item RollItem(Lootbox lootbox)
    {
        var pool = new List<(Item item, float weight)>();

        foreach (var item in lootbox.Items)
        {
            var weight = rarityWeights[item.Rarity];
            pool.Add((item, weight));
        }

        float total = 0;
        foreach (var p in pool) total += p.weight;

        float roll = Random.Range(0, total);
        float current = 0;

        foreach (var p in pool)
        {
            current += p.weight;
            if (roll <= current)
                return p.item;
        }

        return pool[0].item;
    }

    public void GrantItem(Item item)
    {
        playerInventory.Items.Add(item);
    }
}


public class Lootbox
{
    public string Name;
    public List<Item> Items;
}