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

    private void Start() {
        if(!isClient) {
            NetworkManager.singleton.StartHost();
        } else {
            discovery.CreateClient();
        }
    }
}
