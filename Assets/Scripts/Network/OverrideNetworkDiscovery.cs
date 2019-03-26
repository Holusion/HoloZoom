using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OverrideNetworkDiscovery : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        NetworkManager.singleton.networkAddress = fromAddress;
        UnityEngine.Networking.NetworkClient client = NetworkManager.singleton.StartClient();
        client.RegisterHandler(MyMsgType.State, OnStateMsg);
        this.StopBroadcast();
    }

    public void CreateClient() {
        this.Initialize();
        this.StartAsClient();
    }

    void Start() {
        this.showGUI = false;
    }

    void OnStateMsg(NetworkMessage netMsg) {
        StateMessage msg = netMsg.ReadMessage<StateMessage>();
        this.StartCoroutine("Sync", msg);
    }
    
    IEnumerator Sync(StateMessage msg) {
        yield return new WaitForSeconds(2f);
        TargetController controller = GameObject.FindWithTag("Tracker").GetComponent<TargetController>();
        controller.lastTarget.DeSerialize(msg.payload);
    }
}
