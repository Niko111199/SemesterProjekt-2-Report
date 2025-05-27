//dette scipt har til hensigt at være shoppen, hvor vare kommer til syne for brugeren, som så kan købe varen

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab; // Prefab for hver shop item UI
    [SerializeField] private Transform shopContent;     // Hvor i UI'et items skal placeres
    [SerializeField] private ARFaceList itemlist;    // Referencer til shop UI'et
     private IGoldManager goldManager = GoldManager.GetInstance();

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public int cost;
        public Sprite icon; // Hvis man vil vise et billede
        public GameObject item;
    }

    [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();

    private void Start()
    {
        PopulateShop();

        itemlist = FindFirstObjectByType<ARFaceList>();
    }

    private void PopulateShop()
    {
        foreach (ShopItem item in shopItems)
        {

            if (PlayerPrefs.HasKey(item.itemName)) continue; // Hvis item allerede er k�bt, s� spring det over

            GameObject obj = Instantiate(shopItemPrefab, shopContent);
            Debug.Log($"Spawner item: {item.itemName}");

            // Find dele i prefabbet og s�t dem
            obj.transform.Find("ItemName").GetComponent<TMP_Text>().text = item.itemName;
            obj.transform.Find("ItemCost").GetComponent<TMP_Text>().text = item.cost.ToString();
            obj.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;

            Button buyButton = obj.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => TryBuyItem(item));
        }
    }

    private void RestockShop()
    {
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }
        PopulateShop();
    }

    private void TryBuyItem(ShopItem item)
    {
        if (goldManager.GetGoldAmount() >= item.cost)
        {
            if (itemlist != null)
            {
                itemlist.AddFacePrefab(item.item);

                goldManager.SpendGold(item.cost);

                shopItems.Remove(item);

                RestockShop();

                PlayerPrefs.SetInt(item.itemName, 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            Debug.Log("Ikke nok guld til at k�be dette item.");
        }

    }
    

}

//skrevet af: Patrick og Nikolaj Bræmer
// valideret af: TODO: mangler validitering