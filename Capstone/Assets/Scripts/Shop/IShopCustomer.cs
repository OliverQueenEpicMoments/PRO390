using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer {
    void BoughtItem(string itemname);
    bool TrySpendGoldAmmount(int goldammount);
}