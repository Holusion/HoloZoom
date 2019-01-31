using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public List<Interaction> interactions;

    public void UpdateInteractions(Player player)
    {
        foreach (Interaction i in interactions)
        {
            if(i.CanInteract(player)) {
                i.UpdateInteraction(player);
            }
        }
    }
}
