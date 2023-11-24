using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour {
    [SerializeField] private ItemScriptableObject ItemStats;
    [SerializeField] private Item item;

    private SpriteRenderer spriterenderer;

    private void Awake() {
        spriterenderer = GetComponent<SpriteRenderer>();
        //SetItem(ItemStats.item);
    }

    public void SetItem(Item item) { 
        this.item = item;
        this.item.itemtype = ItemStats.ItemType;
        Debug.Log(this.item.itemtype);
        spriterenderer.sprite = item.GetSprite();
    }

    public Item GetItem() { return item; }
}