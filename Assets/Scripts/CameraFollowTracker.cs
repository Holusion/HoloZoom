using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowTracker : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject tracker =  GameObject.Find("Tracker(Clone)");
        if(tracker != null) {
            this.transform.position = tracker.transform.position;
            this.transform.rotation = tracker.transform.rotation;
        }        
    }
}
