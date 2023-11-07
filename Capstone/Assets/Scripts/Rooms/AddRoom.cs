using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {
    private RoomTemplates Templates;

    void Start() {
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Templates.Rooms.Add(this.gameObject);
    }

    void Update() {
        
    }
}