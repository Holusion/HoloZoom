using UnityEditor;

public class InteractionsAsset {

    [MenuItem("Assets/Create/Interactions/Click selector")]
    public static void CreateClickSelectorAsset()
    {
        ScriptableObjectUtility.CreateAsset<ClickSelectionInteraction>();
    }

    [MenuItem("Assets/Create/Interactions/Network")]
    public static void CreateNetworkAsset()
    {
        ScriptableObjectUtility.CreateAsset<NetworkInteraction>();
    }
}
