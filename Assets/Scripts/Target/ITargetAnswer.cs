using UnityEngine;

public interface ITargetAnswer
{
    // Is called when a selection on the current game object is detected (CmdTarget(SELECT, gameObject))
    void OnSelected(GameObject previousTarget);
    // Is called when a reset on the current game object is detected (CmdTarget(UNSELECT, gameObject))
    void OnUnselected(GameObject previouslastTarget);
    // Is called when "enable" change (CmdEnable(gameObject, enable))
    void OnActive(bool enable);
    // Method to use to inform Unity that this.gameObject should be disable (for exemple, event animation) 
    void OnDesactive();
    // Is called when a rotation around the current gameObject is detected
    void OnRotate();
}
