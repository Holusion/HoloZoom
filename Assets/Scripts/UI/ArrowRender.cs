using UnityEngine;

public class ArrowRender : MonoBehaviour
{
    [Range(0,1)]
    public float percentHead = 0.4f;
    public float lineWidth = 0.2f;

    public GameObject target;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.DrawArrow();
    }

    void OnValidate()
    {
        if(this.lineRenderer == null) {
            this.lineRenderer = this.GetComponent<LineRenderer>();
        }
        this.DrawArrow();
    }

    void DrawArrow() 
    {
        Vector3 targetPosition = target.transform.position;
        // targetPosition.y = target.GetComponent<MeshRenderer>().bounds.size.y;
        float percentSize = percentHead / Vector3.Distance(this.transform.position, targetPosition);

        this.lineRenderer.widthCurve = new AnimationCurve(
            new Keyframe(0, lineWidth / 5f),
            new Keyframe(0.999f - percentSize, lineWidth / 5f),
            new Keyframe(1 - percentSize, lineWidth),
            new Keyframe(1, 0)
        );

        this.lineRenderer.SetPosition(0, this.transform.position);
        this.lineRenderer.SetPosition(1, Vector3.Lerp(this.transform.position, targetPosition, 0.999f - percentSize));
        this.lineRenderer.SetPosition(2, Vector3.Lerp(this.transform.position, targetPosition, 1 - percentSize));
        this.lineRenderer.SetPosition(3, targetPosition);
    }
}
