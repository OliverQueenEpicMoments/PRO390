using UnityEngine;

public class Destroyer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Enemy") && !collision.CompareTag("Confiner") && !collision.CompareTag("Merchant") && !collision.CompareTag("NoDestroy")) {
            Destroy(collision.gameObject);
            Debug.Log("Destroyed " + collision);
        }
    }
}