using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrackerSpawner : NetworkBehaviour {
    
    public GameObject trackerPrefab;

    public override void OnStartServer() {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;

        GameObject tracker = (GameObject) Instantiate(trackerPrefab, position, rotation);
        TargetController controller = tracker.GetComponent<TargetController>();
        controller.interactiveTarget = GameObject.Find("InteractiveObjects");
        controller.initPos = GameObject.Find("InitPos");
        NetworkServer.Spawn(tracker);
    }
}