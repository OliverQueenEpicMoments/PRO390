using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    [SerializeField] private float Timer;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private AudioClip SummonSound;

    void Start() {
        SoundManager.Instance.PlaySound(SummonSound);

        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        var Direction = MousePosition - transform.position;
        float Rot = Mathf.Atan2(-Direction.y, -Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Rot - 90);
    }

    void Update() {
        Timer -= Time.deltaTime;

        if (Timer <= 0) {
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<Health>().TakeDamage(2);
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }

        if (collision.CompareTag("Wall")) {
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}