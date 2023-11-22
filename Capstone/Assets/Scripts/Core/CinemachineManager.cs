using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class CinemachineManager : MonoBehaviour {
    [SerializeField] private float TeleportDistance = 4;
    private GameObject Player;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TeleportUp() {
        Player.transform.position = Player.transform.position + (Vector3.up * TeleportDistance);
    }

    public void TeleportDown() {
        Player.transform.position = Player.transform.position + (Vector3.down * TeleportDistance);
    }

    public void TeleportLeft() {
        Player.transform.position = Player.transform.position + (Vector3.left * TeleportDistance);
    }

    public void TeleportRight() {
        Player.transform.position = Player.transform.position + (Vector3.right * TeleportDistance);
    }
}