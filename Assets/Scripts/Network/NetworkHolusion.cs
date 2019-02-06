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

    private void Start() {
        if(!isClient) {
            NetworkManager.singleton.StartHost();
        } else {
            discovery.CreateClient();
        }
    }
}
