using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {
    private static GameAssets _Instance;

    public static GameAssets Instance {
        get {
            if (_Instance == null) _Instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _Instance;
        }
    }

    [Header("Sprites")]
    public Sprite SpikedShieldIcon;
    public Sprite BruiserItemIcon;
    public Sprite BetrayersSwordIcon;

    public Sprite EstusFlask;

    [Header("Item Scriptables")]
    public ItemScriptableObject SpikedShieldStats;
    public ItemScriptableObject BruiserStats;
    public ItemScriptableObject BetrayersSwordStats;

    [Header("Audio")]
    public AudioClip Explosion;
    public AudioClip ItemBought;
    public AudioClip SwordSlash;
    public AudioClip Bashes;

    [Header("Game Objects")]
    public GameObject Merchant;

    [Header("Misc")]
    public UIShop Shop;
}