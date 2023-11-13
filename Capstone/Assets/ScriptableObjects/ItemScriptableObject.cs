using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject {
    public Item.ItemType ItemType;
    public string ItemName;
    public string ItemDesc;
    public Sprite ItemIcon;
    public int ItemCost;

    public GameObject ItemPowerup;

    public float ItemHealth;
    public float ItemMana;
    public float ItemManaRegen;
    public float ItemCDR;
    public float ItemPower;
    public float ItemProtections;
    public float ItemAS;
    public float ItemMS;
    public float ItemPen;
    public float ItemPercentPen;
    public float ItemLifesteal;
}