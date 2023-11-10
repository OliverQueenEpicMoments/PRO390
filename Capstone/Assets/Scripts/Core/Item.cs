using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public static ItemScriptableObject Items;

    public enum ItemType { 
        Tank,
        Bruiser,
        Assassin
    }

    public static int GetCost(ItemType item) { 
        switch (item) {
            default:
            case ItemType.Tank: return 2900;
            case ItemType.Bruiser: return 3000;
            case ItemType.Assassin: return 3000;
        }
    }

    public static Sprite GetSprite() {
        //switch (item) {
        //    default:
        //    case ItemType.Tank: return GameAssets.Instance.Joe;
        //    case ItemType.Bruiser: return GameAssets.Instance.Joe;
        //    case ItemType.Assassin: return GameAssets.Instance.Joe;
        //}

        return Items.ItemIcon;
    }

    public static string GetString(ItemType item) {
        switch (item) {
            default:
            case ItemType.Tank: return "Getting damaged by enemies deals a portion of it back to them";
            case ItemType.Bruiser: return "Deals damage around you based on max health";
            case ItemType.Assassin: return "Your next basic attack after an ability does extra damage";
        }
    }
}