using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour {
    private TextMeshProUGUI GoldText;
    private TextMeshProUGUI EstusFlaskText;

    private void Awake() {
        GoldText = transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
        EstusFlaskText = transform.Find("EstusText").GetComponent<TextMeshProUGUI>();
    }

    void Start() {
        UpdateText();

        PlayerController.Instance.OnGoldAmountChanged += InstanceOnGoldAmountChanged;
        PlayerController.Instance.OnEstusAmountChanged += InstanceOnEstusAmountChanged;
    }

    private void InstanceOnEstusAmountChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void InstanceOnGoldAmountChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void UpdateText() {
        GoldText.text = PlayerController.Instance.GetGoldAmount().ToString();
        EstusFlaskText.text = PlayerController.Instance.GetEstusAmount().ToString();
    }
}