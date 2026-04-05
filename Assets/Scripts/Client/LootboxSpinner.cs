using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootboxSpinner : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject itemSlotPrefab;

    [Header("Spin Settings")]
    [SerializeField] private float spinDuration = 5f;
    [SerializeField] private float itemWidth = 160f;
    [SerializeField] private int fillerItemCount = 40;

    private Item winningItem;

    public void StartSpin(Lootbox lootbox, Item result)
    {
        winningItem = result;
        BuildStrip(lootbox, result);
        StartCoroutine(SpinCoroutine());
    }

    private void BuildStrip(Lootbox lootbox, Item result)
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        var visualList = new List<Item>();

        for (int i = 0; i < fillerItemCount; i++)
            visualList.Add(lootbox.Items[Random.Range(0, lootbox.Items.Count)]);

        int winIndex = visualList.Count / 2;
        visualList.Insert(winIndex, result);

        foreach (var item in visualList)
            CreateSlot(item);
    }

    private void CreateSlot(Item item)
    {
        var slot = Instantiate(itemSlotPrefab, content);
        slot.GetComponentInChildren<TMP_Text>().text = item.Name;

        var img = slot.GetComponent<Image>();
        img.color = GetRarityColor(item.Rarity);
    }

    private IEnumerator SpinCoroutine()
    {
        float elapsed = 0f;
        float startX = content.anchoredPosition.x;
        float targetX = -(itemWidth * (content.childCount / 2));

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / spinDuration;

            float eased = 1 - Mathf.Pow(1 - t, 3);
            float x = Mathf.Lerp(startX, targetX, eased);

            content.anchoredPosition = new Vector2(x, content.anchoredPosition.y);
            yield return null;
        }

        content.anchoredPosition = new Vector2(targetX, content.anchoredPosition.y);
        OnSpinComplete();
    }

    private void OnSpinComplete()
    {
        Debug.Log($"Won: {winningItem.Name}");
    }

    private Color GetRarityColor(ItemRarity rarity)
    {
        return rarity switch
        {
            ItemRarity.Common => Color.gray,
            ItemRarity.Uncommon => Color.green,
            ItemRarity.Rare => Color.blue,
            ItemRarity.Epic => new Color(0.7f, 0f, 1f),
            ItemRarity.Legendary => new Color(1f, 0.6f, 0f),
            ItemRarity.Mythical => Color.red,
            ItemRarity.Unique => Color.cyan,
            _ => Color.white
        };
    }
}
