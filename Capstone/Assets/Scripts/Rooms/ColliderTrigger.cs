using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour {
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<PlayerController>(out var Player)) {
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}