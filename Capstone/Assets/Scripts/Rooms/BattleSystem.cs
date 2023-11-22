using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {
    [SerializeField] private ColliderTrigger Trigger;
    [SerializeField] private Wave[] Waves;
    [SerializeField] private GameObject[] Doors;

    private enum State { 
        Idle,
        Active,
        BattleOver
    }

    private State state;

    private void Awake() {
        state = State.Idle;
    }

    void Start() {
        Trigger.OnPlayerEnterTrigger += Trigger_OnPlayerEnterTrigger;
    }

    private void Trigger_OnPlayerEnterTrigger(object sender, System.EventArgs e) {
        if (state == State.Idle) {
            StartBattle();
            Trigger.OnPlayerEnterTrigger -= Trigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle() {
        Debug.Log("Start Battle");
        state = State.Active;
    }

    private void Update() {
        switch (state) {
            case State.Active:
                foreach (var Wave in Waves) {
                    Wave.Update();
                }
                TestBattleOver();
                break;
        }
    }

    private void TestBattleOver() {
        if (state == State.Active) {
            if (AreWavesOver()) {
                state = State.BattleOver;
                Debug.Log("Battle is over");

                foreach (var Door in Doors) Door.SetActive(true);
            }
        }
    }

    private bool AreWavesOver() {
        foreach (var Wave in Waves) {
            if (Wave.IsWaveOver()) {

            } else {
                return false;
            }
        }
        return true;
    }

    [System.Serializable]
    private class Wave {
        [SerializeField] private GameObject[] Enemies;
        [SerializeField] private float WaveTimer;

        public void Update() {
            if (WaveTimer >= 0) {
                WaveTimer -= Time.deltaTime;
                if (WaveTimer <= 0) {
                    SpawnEnemies();
                }
            }
        }

        public void SpawnEnemies() {
            foreach (var Enemy in Enemies) Enemy.SetActive(true);
        }

        public bool IsWaveOver() {
            if (WaveTimer < 0) {
                foreach (var Enemy in Enemies) {
                    if (Enemy.activeSelf) return false;
                }
                return true;
            } else {
                return false;
            }
        }
    }
}