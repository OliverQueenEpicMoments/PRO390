using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class CameraSwitcher : MonoBehaviour {
    [SerializeField] string Name;
    [SerializeField] private GameObject Player;
    [SerializeField] private BoxCollider2D HitBox;
    [SerializeField] private Transform TeleportLocation;

    private bool MainCamera = true;
    private Animator animator;

    private void Start() {
        //if (CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera == VCamera1) MainCamera = true;
        //else MainCamera = false;

        Debug.Log(Name + " " + MainCamera);
        //Debug.Log(MainCamera);
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) Player = collision.gameObject;
    }

    private void SwitchState() {
        if (MainCamera) {
            animator.Play("Secondary");
        } else {
            animator.Play("Main");
        }
        MainCamera = !MainCamera;
    }

    // Recode this
    public void Interact() {
        SwitchState();
        Player.SetActive(false);
        Player.transform.position = TeleportLocation.position;
        Player.SetActive(true);
    }
}