using UnityEngine;

public abstract class StageAdvacner : MonoBehaviour {
    
    void OnEnable () {
        GlobalEventManager.AddListener("player advance", OnPlayerAdvance);
    }

    void OnDisable () {
        GlobalEventManager.RemoveListener("player advance", OnPlayerAdvance);
    }


    protected abstract void OnPlayerAdvance ();
}