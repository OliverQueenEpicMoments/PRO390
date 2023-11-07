using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    [SerializeField] private int OpeningDirection;
    // 1 = Need bottom door
    // 2 = Need top door
    // 3 = Need left door
    // 4 = Need right door

    private RoomTemplates Templates;
    private int RandomInArray;
    private bool Spawned = false;
    private float WaitTime = 4;

    void Start() {
        Destroy(gameObject, WaitTime);
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke(nameof(Spawn), 0.1f);
    }

    void Spawn() {
        if (Spawned == false) {
            switch (OpeningDirection) {
                case 1:
                    RandomInArray = Random.Range(0, Templates.BottomRooms.Length);
                    Instantiate(Templates.BottomRooms[RandomInArray], transform.position, Quaternion.identity);
                    break;
                case 2:
                    RandomInArray = Random.Range(0, Templates.TopRooms.Length);
                    Instantiate(Templates.TopRooms[RandomInArray], transform.position, Quaternion.identity);
                    break;
                case 3:
                    RandomInArray = Random.Range(0, Templates.LeftRooms.Length);
                    Instantiate(Templates.LeftRooms[RandomInArray], transform.position, Quaternion.identity);
                    break;
                case 4:
                    RandomInArray = Random.Range(0, Templates.RightRooms.Length);
                    Instantiate(Templates.RightRooms[RandomInArray], transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log("Something went wrong...");
                    break;
            }
            Spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("SpawnPoint")) { 
            if (collision.GetComponent<RoomSpawner>().Spawned == false && Spawned == false) {
                Instantiate(Templates.ClosedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Spawned = true;
        }
    }
}