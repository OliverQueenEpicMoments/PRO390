using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class CinemachineManager : MonoBehaviour, IInteractable {
    [SerializeField] string Name;
    [SerializeField] private GameObject Player;
    [SerializeField] private BoxCollider2D HitBox;
    [SerializeField] private Transform TeleportLocation;
    [SerializeField] private CinemachineVirtualCamera Main;
    [SerializeField] private CinemachineVirtualCamera Secondary;
    [SerializeField] private bool MainCamera = true;

    private void Start() {
        //if (CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera == VCamera1) MainCamera = true;
        //else MainCamera = false;

        Debug.Log(Name + " " + MainCamera);
        //Debug.Log(MainCamera);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) Player = collision.gameObject;
    }

    private void SwitchPriority() {
        if (MainCamera) {
            Main.Priority = 0;
            Secondary.Priority = 1;
            Debug.Log(Name + " Camera 2 Priority");
            
        } else {
            Main.Priority = 1;
            Secondary.Priority = 0;
            Debug.Log(Name + " Camera 1 Priority");
        }
        //MainCamera = !MainCamera;
        Debug.Log(Name + " " + MainCamera);
    }

    public void Interact() {
        //if (CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera == Main && MainCamera) {
        //    SwitchPriority();
        //    Player.SetActive(false);
        //    Player.transform.position = TeleportLocation.position;
        //    Player.SetActive(true);
        //}

        SwitchPriority();
        Player.SetActive(false);
        Player.transform.position = TeleportLocation.position;
        Player.SetActive(true);
    }
}