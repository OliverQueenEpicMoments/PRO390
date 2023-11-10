using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour {
    [SerializeField] private ItemScriptableObject Item1;
    [SerializeField] private ItemScriptableObject Item2;

    private Transform Container;
    private Transform ShopItemTemplate;
    private IShopCustomer ShopCustomer;

    private void Awake() {
        Container = transform.Find("Container");
        ShopItemTemplate = Container.Find("ShopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        CreateItemButton(Item1.ItemIcon, Item1.ItemName, Item1.ItemDesc, Item1.ItemCost, 0);
        CreateItemButton(Item2.ItemIcon, Item2.ItemName, Item2.ItemDesc, Item2.ItemCost, 1);

        Hide();
    }

    private void CreateItemButton(Sprite itemsprite, string itemname, string itemdesc, int itemcost, int position) {
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
            TryBuyItem(itemname);
        };
    }

    private void TryBuyItem(string itemname) {
        ShopCustomer.BoughtItem(itemname);
    }

    public void Show(IShopCustomer shopcustomer) { 
        this.ShopCustomer = shopcustomer;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}