﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowTracker : MonoBehaviour
{
    private Camera cam;
    public GameObject initPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject tracker =  GameObject.Find("Tracker(Clone)");
        TargetController controller = tracker.GetComponent<TargetController>();

        if(tracker != null) {
            this.transform.position = tracker.transform.position;
            this.transform.rotation = tracker.transform.rotation;
        }
    }
}
