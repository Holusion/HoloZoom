using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour {

    public const string BUTTON_LEFT = "Fire1";
    public const string BUTTON_RIGHT = "Fire2";
    public const string SELECT = "select";
    public const string UNSELECT = "unselect";
    public const string ROTATE = "rotate";

    private float lastSpeed = 0;

    private InteractionManager interactionManager;

    void Start() {
        this.interactionManager = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
    }

    void Update() {
        this.interactionManager.UpdateInteractions(this);
    }

    public bool IsUI() {
        return EventSystem.current != null && this.IsPointerOverUIObject();
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    [Command]
    public void CmdTarget(string action, GameObject hit) {
        RpcTarget(action, hit);
    }

    [Command]
    public void CmdTargets(string action, string[] hit) {
        RpcTargets(action, hit);
    }

    [Command]
    public void CmdRotate(float speed) {
        RpcRotate(speed);
    }

    [Command]
    public void CmdEnable(GameObject go, bool enable) {
        RpcEnable(go, enable);
    }

    [Command]
    public void CmdAnimate(GameObject go, string trigger) {
        RpcAnimate(go, trigger);
    }

    [ClientRpc]
    public void RpcTarget(string action, GameObject hit) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();

        if(action == SELECT) {
            controller.SetTarget(hit);
        } else if (action == UNSELECT) {
            controller.SetTarget(null, true);
        }
    }

    [ClientRpc]
    public void RpcTargets(string action, string[] hit) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        GameObject selected = null;

        foreach(string s in hit) {
            selected = controller.target.GetComponent<Activator>().nextSelectable.Find(x => x.gameObject.name == s).gameObject;
            if(selected != null) {
                break;
            }
        }

        if(action == SELECT && selected != null) {
            controller.SetTarget(selected);
        } else if (action == UNSELECT) {
            controller.SetTarget(null, true);
        }
    }

    [ClientRpc]
    public void RpcRotate(float speed) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.RotateTarget(speed);
    }

    [ClientRpc]
    public void RpcEnable(GameObject go, bool enable) {
        Debug.Log(go);

        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.Enable(go, enable);
    }

    [ClientRpc]
    public void RpcAnimate(GameObject go, string trigger) {
        int iTrigger = Animator.StringToHash(trigger);
        Animator anim = go.GetComponent<Animator>();
        anim.SetTrigger(iTrigger);
    }
}