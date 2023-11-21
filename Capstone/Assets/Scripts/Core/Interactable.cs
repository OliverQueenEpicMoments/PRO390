using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    public bool IsInRange;
    public KeyCode InteractButton;
    public UnityEvent InteractAction;

    void Update() {
        if (IsInRange) {
            if (Input.GetKeyDown(InteractButton)) {
                InteractAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) { 
            IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            IsInRange = false;
        }
    }
}