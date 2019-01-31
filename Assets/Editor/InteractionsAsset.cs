using UnityEditor;

public class InteractionsAsset {

    [MenuItem("Assets/Create/Interactions/Click selector")]
    public static void CreateClickSelectorAsset()
    {
        ScriptableObjectUtility.CreateAsset<ClickSelectionInteraction>();
    }

    [MenuItem("Assets/Create/Interactions/Button activation")]
    public static void CreateButtonActivationAsset()
    {
        ScriptableObjectUtility.CreateAsset<ButtonActivationInteraction>();
    }
}
