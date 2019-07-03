using UnityEngine;

public abstract class Interaction : ScriptableObject
{

    protected Player player;

    public abstract void UpdateInteraction(Player player);
    public abstract bool CanInteract(Player player);
}
