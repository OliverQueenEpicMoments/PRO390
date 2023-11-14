using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public enum ItemType { 
        SpikedShield,
        Bruiser,
        BetrayersSword,
        EstusFlask
    }

    public static int GetCost(ItemType item) { 
        switch (item) {
            default:
            case ItemType.SpikedShield: return 2750;
            case ItemType.Bruiser: return 2900;
            case ItemType.BetrayersSword: return 3000;
            case ItemType.EstusFlask: return 5000;
        }
    }

    public static Sprite GetSprite(ItemType item) {
        switch (item) {
            default:
            case ItemType.SpikedShield: return GameAssets.Instance.SpikedShieldIcon;
            case ItemType.Bruiser: return GameAssets.Instance.BruiserItemIcon;
            case ItemType.BetrayersSword: return GameAssets.Instance.BetrayersSwordIcon;
            case ItemType.EstusFlask: return GameAssets.Instance.EstusFlask;
        }
    }

    public static string GetDescription(ItemType item) {
        switch (item) {
            default:
            case ItemType.SpikedShield: return "Getting damaged by enemies deals a portion of it back to them";
            case ItemType.Bruiser: return "Deals damage around you based on max health";
            case ItemType.BetrayersSword: return "Your next basic attack after an ability does extra damage";
            case ItemType.EstusFlask: return "A singular but potent healing vial, expensive due to its rarity";
        }
    }
}