using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour {
    private Transform Container;
    private Transform ShopItemTemplate;
    private IShopCustomer ShopCustomer;

    private void Awake() {
        Container = transform.Find("Container");
        ShopItemTemplate = Container.Find("ShopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        CreateItemButton(Item.ItemType.SpikedShield, Item.GetSprite(Item.ItemType.SpikedShield), "Spiked Shield", Item.GetDescription(Item.ItemType.SpikedShield), Item.GetCost(Item.ItemType.SpikedShield), 0);
        CreateItemButton(Item.ItemType.Bruiser, Item.GetSprite(Item.ItemType.Bruiser), "Bruiser item", Item.GetDescription(Item.ItemType.Bruiser), Item.GetCost(Item.ItemType.Bruiser), 1);
        CreateItemButton(Item.ItemType.BetrayersSword, Item.GetSprite(Item.ItemType.BetrayersSword), "Betrayers Sword", Item.GetDescription(Item.ItemType.BetrayersSword), Item.GetCost(Item.ItemType.BetrayersSword), 2);

        CreateItemButton(Item.ItemType.EstusFlask, Item.GetSprite(Item.ItemType.EstusFlask), "Healing vial", Item.GetDescription(Item.ItemType.EstusFlask), Item.GetCost(Item.ItemType.EstusFlask), 3);

        Hide();
    }

    private void CreateItemButton(Item.ItemType itemtype, Sprite itemsprite, string itemname, string itemdesc, int itemcost, int position) {
        Transform ShopItemTransform = Instantiate(ShopItemTemplate, Container);
        ShopItemTransform.gameObject.SetActive(true);
        RectTransform ShopItemRectTransform = ShopItemTransform.GetComponent<RectTransform>();

        float ShopItemHeight = 200;
        ShopItemRectTransform.anchoredPosition = new Vector2(0, -ShopItemHeight * position);

        ShopItemTransform.Find("ItemNameText").GetComponent<TextMeshProUGUI>().SetText(itemname);
        ShopItemTransform.Find("ItemDescText").GetComponent<TextMeshProUGUI>().SetText(itemdesc);
        ShopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemcost.ToString());
        ShopItemTransform.Find("ItemIcon").GetComponent<Image>().sprite = itemsprite;

        ShopItemTransform.GetComponent<Button_UI>().ClickFunc = () => {
            TryBuyItem(itemtype);
        };
    }

    private void TryBuyItem(Item.ItemType itemtype) {
        if (ShopCustomer.TrySpendGoldAmmount(Item.GetCost(itemtype))) ShopCustomer.BoughtItem(itemtype);
    }

    public void Show(IShopCustomer shopcustomer) { 
        ShopCustomer = shopcustomer;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}