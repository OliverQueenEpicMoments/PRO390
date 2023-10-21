using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class Interactor : MonoBehaviour {
    [SerializeField] private Transform InteractorSource;
    [SerializeField] float InteractRange;

    void Update() {
        Debug.DrawRay(InteractorSource.position, Vector2.down * InteractRange, Color.red);
        Debug.DrawRay(InteractorSource.position, Vector2.left * InteractRange, Color.red);
        if (Input.GetKeyUp(KeyCode.E)) {
            RaycastHit2D HitInfo = Physics2D.Raycast(InteractorSource.position, Vector2.down, InteractRange);
            if (HitInfo.collider.gameObject.TryGetComponent(out IInteractable InteractObj)) {
                InteractObj.Interact();
                Debug.DrawRay(InteractorSource.position, Vector2.down * InteractRange, Color.red);
            }
        }
    }
}