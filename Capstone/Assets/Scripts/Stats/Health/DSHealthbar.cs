using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DSHealthbar : MonoBehaviour {
    [SerializeField] Slider HealthSlider;
    [SerializeField] Slider EaseHealthSlider;
    [SerializeField] private Health PlayerHealth;
    [SerializeField] float LerpSpeed = 0.05f;

    void Start() {
        //Health = PlayerHealth.StartingHealth;
    }

    void Update() {
        if (HealthSlider.value != PlayerHealth.CurrentHealth) {
            HealthSlider.value = PlayerHealth.CurrentHealth;
        }

        if (HealthSlider.value != EaseHealthSlider.value) {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, PlayerHealth.CurrentHealth, LerpSpeed);
        }
    }
}