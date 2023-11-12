using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer {
    void BoughtItem(Item.ItemType itemtype);
    bool TrySpendGoldAmmount(int goldammount);
}