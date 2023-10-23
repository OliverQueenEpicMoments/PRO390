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

    Vector3 InitialPosition;

    void Start() {
        InitialPosition = transform.position;
        StartCoroutine(Explode());
    }

    void Update() {
        if (Rise) {
            Vector3 Offset = CurrentSpeed * Time.deltaTime * Vector2.up;
            CurrentSpeed += Acceleration * Time.deltaTime;

            if (CurrentSpeed > MaxSpeed) CurrentSpeed = MaxSpeed;

            transform.position += Offset;
        }
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(Timer);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}