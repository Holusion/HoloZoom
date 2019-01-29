using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DollyZoom : MonoBehaviour {

    public TargetController targetBehavior;
    public float zoomSpeed = 5f;
    
    private Camera cam;
    private float initHeightAtDist;
    bool enableDolly = false;

    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(this.cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    void Start()
    {
        this.cam = this.GetComponent<Camera>();
    }

    void StartDZ()
    {
        this.enableDolly = true;
        float distance = Vector3.Distance(this.transform.position, this.targetBehavior.target.transform.position);
        this.initHeightAtDist = this.FrustumHeightAtDistance(distance);
    }

    void StopDZ()
    {
        this.enableDolly = false;
    }

    // Update is called once per frame
    void Update () {
        if(this.enableDolly)
        {
            float currDistance = Vector3.Distance(transform.position, this.targetBehavior.target.transform.position);
            this.cam.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
        }

        this.transform.Translate(Input.GetAxis("Vertical") * this.transform.forward * Time.deltaTime * this.zoomSpeed, Space.World);
	}

    public void TargetChange(GameObject target)
    {
        StopDZ();
    }

    public void TargetHasChanged(GameObject target)
    {
        StartDZ();
    }

    public void Reset()
    {
        StopDZ();
    }
}
