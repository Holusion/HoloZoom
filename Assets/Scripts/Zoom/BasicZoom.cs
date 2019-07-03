using UnityEngine;

public class BasicZoom : MonoBehaviour {

    public float zoomSpeed = 5f;
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Input.GetAxis("Vertical") * this.transform.forward * Time.deltaTime * this.zoomSpeed, Space.World);
    }
}
