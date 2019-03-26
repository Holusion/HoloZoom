using UnityEngine;
using UnityEngine.Networking;

public class NetworkHolusion : NetworkManager {
    
    public OverrideNetworkDiscovery discovery;
    public bool isClient;

	public override void OnStartHost()
	{
		discovery.Initialize();
		discovery.StartAsServer();
	}

    public override void OnClientDisconnect(NetworkConnection conn) {
        base.OnServerDisconnect(conn);
        if(isClient) {
            discovery.CreateClient();
        }
    }

    public override void OnServerReady(NetworkConnection conn) {
        base.OnServerReady(conn);
        if(!isClient) {
            byte[] payload = GameObject.FindWithTag("Tracker").GetComponent<TargetController>().lastTarget.Serialize();
            StateMessage msg = new StateMessage();
            msg.payload = payload;
            Debug.Log("sendmsg");
            NetworkServer.SendToClient(conn.connectionId, MyMsgType.State, msg);
        }

    }

    private void Start() {
        if(!isClient) {
            NetworkManager.singleton.StartHost();
        } else {
            discovery.CreateClient();
        }
    }
}
