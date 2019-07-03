using UnityEngine;

public class BubbleTextBehaviour : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Camera.main.transform.position;
        targetPosition.y = this.transform.position.y;

        this.transform.LookAt(targetPosition);
    }
}
