using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {
    [SerializeField] private GameObject Enemy;

    void Start() {
        StartBattle();
    }

    void Update() {
        
    }

    private void StartBattle() {
        Debug.Log("Start Battle");
        Enemy.SetActive(true);
    }
}