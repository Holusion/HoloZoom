using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHackCamera : MonoBehaviour
{
    public float rayInterval = 0.02f;
    public float maxDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach(GameObject go in walls) {
            go.GetComponent<MeshRenderer>().enabled = true;
        }

        Vector3 leftPos = this.transform.position - Camera.main.transform.right * rayInterval - Camera.main.transform.up * rayInterval;
        Vector3 rightPos = this.transform.position + Camera.main.transform.right * rayInterval - Camera.main.transform.up * rayInterval;
        Vector3 upPos = this.transform.position + Camera.main.transform.up * rayInterval;

        Ray rayLeft = new Ray(leftPos, this.transform.forward);
        Ray rayRight = new Ray(rightPos, this.transform.forward);
        Ray rayUp = new Ray(upPos, this.transform.forward);

        Debug.DrawRay(leftPos, this.transform.forward, Color.cyan);
        Debug.DrawRay(rightPos, this.transform.forward, Color.cyan);
        Debug.DrawRay(upPos, this.transform.forward, Color.cyan);

        RemoveFromRay(rayLeft);
        RemoveFromRay(rayRight);
        RemoveFromRay(rayUp);
    }

    void RemoveFromRay(Ray ray) {
        RaycastHit rayhit;
        if (Physics.Raycast(ray, out rayhit, maxDistance, 1 << 9)) {
            MeshRenderer renderer = rayhit.transform.gameObject.GetComponent<MeshRenderer>();
            renderer.enabled = false;  
        }
    }
}
