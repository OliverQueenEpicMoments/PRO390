using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrioritizer : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera RoomCamera;

    private GameObject Player;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            RoomCamera.Priority = 1;
            RoomCamera.Follow = Player.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) RoomCamera.Priority -= 1;
    }
}