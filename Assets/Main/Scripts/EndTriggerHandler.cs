using UnityEngine;

public class EndTriggerHandler : MonoBehaviour {
    
    bool _hasTriggered = false;

    void OnTriggerEnter(Collider other) {

        if (_hasTriggered)
            return;

        GlobalEventManager.TriggerEvent("terrain end");
        _hasTriggered = true;

    }

}