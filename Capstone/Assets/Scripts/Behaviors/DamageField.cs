using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour {
    [SerializeField] private float Size = 1;
    [SerializeField] private AudioClip ActiveSound;
    [SerializeField] private float Timer = 5;
    private GameObject Player;


    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");

        transform.localScale *= Size;
        //SoundManager.Instance.PlaySound(ActiveSound);
    }

    void Update() {
        Timer -= Time.deltaTime;
        transform.position = Player.transform.position - new Vector3(0, 1.25f, 0);

        if (Timer <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        TimeTickSystem.OnTick5 += delegate (object sender, TimeTickSystem.OnTickEventArgs e) {
            if (collision.CompareTag("Enemy")) collision.GetComponent<Health>().TakeDamage(1f);
        };
    }
}