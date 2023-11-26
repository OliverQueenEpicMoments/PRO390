using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour {
    [SerializeField] private float Size = 1;
    [SerializeField] private float PowerScaling = 1.5f;
    [SerializeField] private AudioClip ActiveSound;
    [SerializeField] private float Timer = 5;

    private GameObject Player;
    private float Damage;

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");

        transform.localScale *= Size;
        if (ActiveSound != null) SoundManager.Instance.PlaySound(ActiveSound);
    }

    void Update() {
        Timer -= Time.deltaTime;
        transform.position = Player.transform.position - new Vector3(0, 1.25f, 0);

        if (Timer <= 0) {
            Destroy(gameObject);
            TimeTickSystem.OnTick5 -= null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Damage = Player.GetComponent<ComboCharacter>().Power * PowerScaling;

        TimeTickSystem.OnTick5 += delegate (object sender, TimeTickSystem.OnTickEventArgs e) {
            if (collision.CompareTag("Enemy") && collision.gameObject.activeSelf) collision.GetComponent<Health>().TakeDamage(Damage);
        };
    }
}