using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class CinemachineManager : MonoBehaviour, IInteractable {
    [SerializeField] private GameObject Player;
    [SerializeField] private BoxCollider2D HitBox;
    [SerializeField] private Transform TeleportLocation;
    [SerializeField] private CinemachineVirtualCamera Main;
    [SerializeField] private CinemachineVirtualCamera Secondary;
    [SerializeField] private bool MainCamera = true;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) Player = collision.gameObject;
        Debug.Log("Player Set");
    }

    private void SwitchPriority() {
        if (MainCamera) {
            Main.Priority = 0;
            Secondary.Priority = 1;
        } else {
            Main.Priority = 1;
            Secondary.Priority = 0;
        }
    }

    public void Interact() {
        SwitchPriority();
        Player.SetActive(false);
        Player.transform.position = TeleportLocation.position;
        Player.SetActive(true);
    }
}