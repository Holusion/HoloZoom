using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetController : MonoBehaviour {

    public GameObject interactiveTarget;
    public GameObject target;
    public float rotationSpeed = 0.1f;
    public float zoomSpeed = 5f;
    public float maxAngularSpeed = 10f;
    bool targetChange = false;
    bool reset = true;
    Quaternion initRotation;
    Stack<GameObject> lastTarget;
    public GameObject initPos;

    public bool readyToRotate = false;

    void Awake()
    {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;

        this.interactiveTarget = GameObject.Find("InteractiveObjects");
        this.initPos = GameObject.Find("InitPos");
    }

    // Use this for initialization
    void Start () {
        this.initRotation = transform.rotation;
        this.interactiveTarget = GameObject.Find("InteractiveObjects");
        this.initPos = GameObject.Find("InitPos");
        this.target = initPos;
        this.lastTarget = new Stack<GameObject>();
        this.target = this.initPos;
	}
	
	// Update is called once per frame
	void Update () {
        if(targetChange)
        {
            Vector3 direction;
            Vector3 targetPosition;
            Quaternion toRotation;

            if(target.GetComponent<BoxCollider>() != null) {
                direction = target.GetComponent<BoxCollider>().bounds.center - transform.position;
                targetPosition = target.transform.Find("CamPos").transform.position;
                toRotation = Quaternion.LookRotation(direction);
            } else {
                targetPosition = target.transform.position;
                toRotation = initRotation;
            }

            this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            this.readyToRotate = false;

            transform.position = Vector3.Lerp(transform.position,targetPosition, zoomSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1 && Quaternion.Angle(toRotation, transform.rotation) < 1)
            {
                targetChange = false;
                this.readyToRotate = true;
            }
        }
    }

    public void SetTarget(GameObject go, bool reset = false)
    {
        int index = target.GetComponent<Activator>().nextSelectable.FindIndex(ao => ao.gameObject == go); 

        if(this.lastTarget.Count == 0 && go != null || index >= 0 && !reset) {
            targetChange = true;
            lastTarget.Push(this.target);
            this.target = go;
            ITargetAnswer answer = this.target.GetComponent<ITargetAnswer>();
            if(answer != null) {
                answer.OnSelected(lastTarget.Peek());
            }
        } else if(reset && this.lastTarget.Count > 0) {
            GameObject previousTarget = lastTarget.Pop();
            ITargetAnswer answer = this.target.GetComponent<ITargetAnswer>();
            if(answer != null) {
                answer.OnUnselected(previousTarget);
            }
            targetChange = true;
            GameObject tmp = this.target;
            this.target = previousTarget;
            if(lastTarget.Count > 0) {
                this.target.GetComponent<ITargetAnswer>().OnSelected(lastTarget.Peek());
            }
        }
        
        this.reset = reset;
    }

    public void RotateTarget(float speed)
    {
        float tmpSpeed = speed;
        Vector3 rotateAroundPoint = new Vector3();

        if(this.target.GetComponent<BoxCollider>() != null) {
            rotateAroundPoint = this.target.transform.TransformPoint(this.target.GetComponent<BoxCollider>().center);
            ITargetAnswer answer = target.GetComponent<ITargetAnswer>();
            if(answer != null) {
                answer.OnRotate();
            }
        } else {
            foreach(Activator.ActivatorObject ao in this.target.GetComponent<Activator>().nextSelectable) {
                rotateAroundPoint += ao.gameObject.transform.position;
            }
            rotateAroundPoint /= this.target.GetComponent<Activator>().nextSelectable.Count;
        }

        if(speed >= maxAngularSpeed) {
            tmpSpeed = maxAngularSpeed;
        }
        if(speed <= -maxAngularSpeed) {
            tmpSpeed = -maxAngularSpeed;
        }
        this.transform.RotateAround(rotateAroundPoint, Vector3.up, tmpSpeed);

    }

    public void Enable(GameObject go, bool enable) {
        ITargetAnswer answer = go.GetComponent<ITargetAnswer>();
        if(answer != null) {
            answer.OnActive(enable);
        }
    }
}
