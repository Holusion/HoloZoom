using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour {

    public const string BUTTON_LEFT = "Fire1";
    public const string BUTTON_RIGHT = "Fire2";
    private float lastSpeed = 0;

    private InteractionManager interactionManager;

    void Start() {
        this.interactionManager = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
    }

    void Update() {
        if(this.isLocalPlayer) {
            this.interactionManager.UpdateInteractions(this);
        }
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
    public void CmdTarget(GameObject hit) {
        RpcTarget(hit);
    }

    [Command]
    public void CmdReset() {
        RpcReset();
    }

    [Command]
    public void CmdTargets(string[] hit) {
        RpcTargets(hit);
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
    public void CmdAnimate(GameObject go, string triggerOn, string triggerOff, bool shouldStack) {
        RpcAnimate(go, triggerOn, triggerOff, shouldStack);
    }

    [Command]
    public void CmdWakeUp() {
        RpcWakeUp();
    }

    [ClientRpc]
    public void RpcTarget(GameObject hit) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.SetTarget(hit);
    }

    [ClientRpc]
    public void RpcReset() {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.Reset();
    }

    [ClientRpc]
    public void RpcTargets(string[] hit) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        GameObject selected = null;

        foreach(string s in hit) {
            selected = controller.target.GetComponent<Activator>().nextSelectable.Find(x => x.gameObject.name == s).gameObject;
            if(selected != null) {
                break;
            }
        }

        if(selected != null) {
            controller.SetTarget(selected);
        }
    }

    [ClientRpc]
    public void RpcRotate(float speed) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.RotateTarget(speed);
    }

    [ClientRpc]
    public void RpcEnable(GameObject go, bool enable) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.Enable(go, enable);
    }

    [ClientRpc]
    public void RpcAnimate(GameObject go, string triggerOn, string triggerOff, bool shouldStack) {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.Animate(go, triggerOn, triggerOff, shouldStack);
    }

    [ClientRpc]
    public void RpcWakeUp() {
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.WakeUp();
    }
}