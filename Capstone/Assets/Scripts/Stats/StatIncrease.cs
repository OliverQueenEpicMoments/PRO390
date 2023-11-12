using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatIncrease : MonoBehaviour {
    public ItemStatEffect StatEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
        StatEffect.Apply(collision.gameObject);
    }
}