using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour {
    private Transform Container;
    private Transform ShopItemTemplate;

    private void Awake() {
        Container = transform.Find("Container");
        ShopItemTemplate = transform.Find("ShopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);
    }

    void Update() {
        
    }
}