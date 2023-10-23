using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour {
    [SerializeField] private float MaxMana = 100;
    [SerializeField] private float StartingMana = 100;
    [SerializeField] private float ManaRegenRate = 100;

    [SerializeField] private Slider ManaBar2D;
    [SerializeField] private Slider ManaBar3D;
    [SerializeField] private TMP_Text ManaText2D;

    private float CurrentMana;

    void Start() {
        CurrentMana = StartingMana;
        UpdateManaUI();
    }

    void Update() {
        
    }

    public void UpdateManaUI() { 
        
    }
}