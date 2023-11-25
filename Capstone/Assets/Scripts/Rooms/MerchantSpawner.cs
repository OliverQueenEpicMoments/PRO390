using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawner : MonoBehaviour {
    [SerializeField] private ColliderTrigger Trigger;
    [SerializeField] private Transform MerchantLocation;

    private GameObject Merchant;

    void Start() {
        Trigger.OnPlayerEnterTrigger += Trigger_OnPlayerEnterTrigger;
    }

    private void Trigger_OnPlayerEnterTrigger(object sender, System.EventArgs e) {
        Merchant = GameObject.FindGameObjectWithTag("Merchant");
        Debug.Log("Merchant Status - " + Merchant);
        if (Merchant == null) Instantiate(GameAssets.Instance.Merchant, MerchantLocation);
        Trigger.OnPlayerEnterTrigger -= Trigger_OnPlayerEnterTrigger;
    }
}