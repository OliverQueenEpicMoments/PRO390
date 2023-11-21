using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class CinemachineManager : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera CurrentCamera;
    [SerializeField] private CinemachineVirtualCamera MainVirtCamera;
    [SerializeField] private CinemachineVirtualCamera SecondaryCamera;
    [SerializeField] private bool ShouldSwap = true;

    private GameObject Player;
    private Camera MainCamera;
    private CinemachineBrain Brain;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = Camera.main;
        Brain = MainCamera.GetComponent<CinemachineBrain>();
    }

    private void Update() {
        MainVirtCamera = Brain.ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    //private IEnumerator Start() {
    //    yield return null;
    //    MainVirtCamera = Brain.ActiveVirtualCamera as CinemachineVirtualCamera;
    //    Debug.Log("Main camera is " + MainVirtCamera);
    //}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Confiner")) {
            // Get other rooms camera
            Debug.Log(gameObject + "'s Secondary camera starts as " + SecondaryCamera);
            SecondaryCamera = collision.GetComponentInChildren<CinemachineVirtualCamera>();
            Debug.Log(gameObject + "'s Secondary camera becomes " + SecondaryCamera);
        }
    }

    private void SwitchPriority() {
        if (!ShouldSwap) {
            MainVirtCamera.Priority = 0;
            SecondaryCamera.Priority = 1;
            SecondaryCamera.Follow = Player.transform;
        } else {
            MainVirtCamera.Priority = 1;
            SecondaryCamera.Priority = 0;
            MainVirtCamera.Follow = Player.transform;
        }
    }

    public void TeleportUp() {
        Player.transform.position = Player.transform.position + (Vector3.up * 8);
        SwitchPriority();
    }

    public void TeleportDown() {
        Player.transform.position = Player.transform.position + (Vector3.down * 3);
        SwitchPriority();
    }

    public void TeleportLeft() {
        Player.transform.position = Player.transform.position + (Vector3.left * 3);
        SwitchPriority();
    }

    public void TeleportRight() {
        Player.transform.position = Player.transform.position + (Vector3.right * 3);
        SwitchPriority();
    }
}