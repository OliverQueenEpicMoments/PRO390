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

    public Sprite SpikedShieldIcon;
    public Sprite BruiserItemIcon;
    public Sprite BetrayersSwordIcon;

    public ItemScriptableObject SpikedShieldStats;
    public ItemScriptableObject BruiserStats;
    public ItemScriptableObject BetrayersSwordStats;

    public Sprite EstusFlask;
}