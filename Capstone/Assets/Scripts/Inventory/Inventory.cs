using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory  {
    public event EventHandler OnItemListChanged;

    private List<Item> ItemList;

    public Inventory() {
        ItemList = new List<Item>();
    }

    public void AddItem(Item item) {
        ItemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList() { 
        return ItemList;
    }
}