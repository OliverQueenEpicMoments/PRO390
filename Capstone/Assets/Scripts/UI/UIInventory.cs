using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {
    private Inventory inventory;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;

    private void Awake() {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory) { 
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems() {
        foreach (Transform Child in ItemSlotContainer) {
            if (Child == ItemSlotTemplate) continue;
            Destroy(Child.gameObject);
        }

        int X = 0;
        int Y = 0;
        float ItemSlotSize = 150f;

        foreach (var Item in inventory.GetItemList()) {
            RectTransform ItemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);
            ItemSlotRectTransform.anchoredPosition = new Vector2(X * ItemSlotSize, Y * ItemSlotSize);
            Image Icon = ItemSlotRectTransform.Find("ItemIcon").GetComponent<Image>();
            Icon.sprite = Item.GetSprite(Item.itemtype);

            X++;
            if (X > 2) {
                X = 0;
                Y++;
            }
        }
    }
}
