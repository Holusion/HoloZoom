using UnityEngine;

public class TrackerSpawner : MonoBehaviour {
    
    public GameObject trackerPrefab;
    public GameObject map;

    // public override void OnStartServer() {
    //     Vector3 position = this.transform.position;
    //     Quaternion rotation = this.transform.rotation;

    //     GameObject tracker = (GameObject) Instantiate(trackerPrefab, position, rotation);
    //     TargetController controller = tracker.GetComponent<TargetController>();
    //     controller.interactiveTarget = GameObject.Find("InteractiveObjects");
    //     controller.initPos = GameObject.Find("InitPos");
    //     controller.map = map;

    //     NetworkServer.Spawn(tracker);
    // }
}