using UnityEngine;

public class SelectableCar : MonoBehaviour {
    
    public GameObject targetPrefab;

    void OnMouseDown () {
        SelectionsManager.carPrefab = targetPrefab;
        print("selected");

        GlobalEventManager.TriggerEvent("car selected");
    }

}