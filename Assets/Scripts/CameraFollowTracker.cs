using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowTracker : MonoBehaviour
{
    [Header("Camera Settings")]
    public float minFarClip = 25;
    public float maxFarClip = 300;
    public float minFOV = 25;
    public float maxFOV = 60;

    // Start is called before the first frame update
    void Start()
    {

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

    public void ChangeFarClip(float targetFarPlane, float distance) {
        Camera.main.farClipPlane += (targetFarPlane - Camera.main.farClipPlane) / (distance + 1);
    }

    public void ChangeFOV(float targetFOV, float distance) {
        Camera.main.fieldOfView += (targetFOV - Camera.main.fieldOfView) / (distance + 1);
        Mathf.Clamp(Camera.main.fieldOfView, minFOV, maxFOV);
    }
}
