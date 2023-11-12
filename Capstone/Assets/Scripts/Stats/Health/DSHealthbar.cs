using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DSHealthbar : MonoBehaviour {
    [SerializeField] Slider HealthSlider;
    [SerializeField] Slider EaseHealthSlider;
    [SerializeField] private Health PlayerHealth;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] bool HealthNumber = true;
    [SerializeField] float LerpSpeed = 0.05f;

    void Update() {
        if (EaseHealthSlider.maxValue != PlayerHealth.MaxHealth) EaseHealthSlider.maxValue = PlayerHealth.MaxHealth;
        if (HealthSlider.maxValue != PlayerHealth.MaxHealth) HealthSlider.maxValue = PlayerHealth.MaxHealth;
        if (HealthSlider.value != PlayerHealth.CurrentHealth) HealthSlider.value = PlayerHealth.CurrentHealth;

        if (HealthSlider.value != EaseHealthSlider.value) {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, PlayerHealth.CurrentHealth, LerpSpeed);
        }

        if (HealthNumber && HealthText != null) HealthText.text = Mathf.RoundToInt(PlayerHealth.CurrentHealth).ToString() + " / " + PlayerHealth.MaxHealth;
    }
}