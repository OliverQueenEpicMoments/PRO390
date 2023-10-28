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
        RegenMana();
    }

    private void RegenMana() { 
        if (CurrentMana < MaxMana) {
            CurrentMana += ManaRegenRate * Time.deltaTime;
            CurrentMana = Mathf.Clamp(CurrentMana, 0f, MaxMana);
            UpdateManaUI();
        }
    }

    public void UpdateManaUI() { 
        if (ManaBar2D != null) ManaBar2D.value = CurrentMana / MaxMana;
        if (ManaBar2D != null) ManaText2D.text = Mathf.RoundToInt(CurrentMana).ToString() + " / " + MaxMana;
        if (ManaBar3D != null) ManaBar3D.value = CurrentMana / MaxMana;
    }

    public bool CanAffordAbility(float abilitycost) {
        return CurrentMana >= abilitycost;
    }

    public void UseAbility(float abilitycost) { 
        CurrentMana -= abilitycost;
        CurrentMana = Mathf.Clamp(CurrentMana, 0f, MaxMana);
        UpdateManaUI();
    }
}