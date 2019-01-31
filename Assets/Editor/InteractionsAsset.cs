using UnityEditor;

public class InteractionsAsset {

    [MenuItem("Assets/Create/Interactions/Click selector")]
    public static void CreateClickSelectorAsset()
    {
        ScriptableObjectUtility.CreateAsset<ClickSelectionInteraction>();
    }
}
