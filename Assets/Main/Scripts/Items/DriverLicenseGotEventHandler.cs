using UnityEngine;

public class DriverLicenseGotEventHandler : MonoBehaviour {
    
    public Item item;


    void OnEnable () {
        item.BeenPicked += OnItemBePicked;
    }


    void OnDisable () {
        item.BeenPicked -= OnItemBePicked;
    }

    void OnItemBePicked () {
        GlobalEventManager.TriggerEvent("got driver license");
    }
}