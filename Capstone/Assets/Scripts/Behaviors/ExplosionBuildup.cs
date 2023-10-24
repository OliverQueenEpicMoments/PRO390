using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosionBuildup : MonoBehaviour {
    [SerializeField] private bool Rise = true;
    [SerializeField] private float Timer;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float Acceleration = 0.01f;
    [SerializeField] private float CurrentSpeed;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private AudioClip SummonSound;

    Vector3 InitialPosition;

    void Start() {
        InitialPosition = transform.position;
        SoundManager.Instance.PlaySound(SummonSound);
    }

    void Update() {
        Timer -= Time.deltaTime;

        if (Rise) {
            Vector3 Offset = CurrentSpeed * Time.deltaTime * Vector2.up;
            CurrentSpeed += Acceleration * Time.deltaTime;

            if (CurrentSpeed > MaxSpeed) CurrentSpeed = MaxSpeed;

            transform.position += Offset;
        }

        if (Timer <= 0) {
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}