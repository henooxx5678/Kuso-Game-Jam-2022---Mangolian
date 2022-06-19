using UnityEngine;

public class EndHandler : MonoBehaviour {
    
    bool _isEnded = false;

    void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (!_isEnded) {
                _isEnded = true;
                GlobalEventManager.TriggerEvent("terrain end");
            }
        }
    }

}