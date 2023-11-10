using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AbilityScriptableObject", menuName = "ScriptableObjects/AbilityScriptableObject")]
public class AbilityScriptableObject : ScriptableObject {
    public string AbilityName;
    public string AbilityDesc;

    public Sprite AbilityIcon;
    public TMP_Text AbilityText;
    public KeyCode AbilityKey;
    public float AbilityCooldown;
    public float AbilityCost;
    public Canvas AbilityCanvas;
    public Image AbilityTargeter;

    public float MaxAbilityRange;
    public float AbilitySpeed;
    public AudioClip EmpowerSound;
    public float EmpoweredDuration;
    public GameObject AbilitySpawn;
}