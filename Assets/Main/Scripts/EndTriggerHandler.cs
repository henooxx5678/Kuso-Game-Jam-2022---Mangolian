using UnityEngine;

public class EndTriggerHandler : MonoBehaviour {
    


    void OnTriggerEnter(Collider other) {

        GlobalEventManager.TriggerEvent("terrain end");

    }

}