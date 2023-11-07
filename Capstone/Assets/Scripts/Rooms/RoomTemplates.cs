using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {
    [Header("Rooms")]
    public GameObject[] BottomRooms;
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;
    public GameObject ClosedRoom;

    [Header("Other")]
    [SerializeField] private float WaitTime = 2;
    [SerializeField] private GameObject Boss;

    public List<GameObject> Rooms;

    private bool SpawnedBoss;

    void Update() {
        if (WaitTime <= 0 && !SpawnedBoss) {
            Instantiate(Boss, Rooms[Rooms.Count - 1].transform.position, Quaternion.identity);
            SpawnedBoss = true;
        } 
        else WaitTime -= Time.deltaTime;
    }
}