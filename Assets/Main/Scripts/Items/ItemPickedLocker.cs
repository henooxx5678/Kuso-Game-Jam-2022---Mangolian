using UnityEngine;

public class ItemPickedLocker : MonoBehaviour {
    
    public Item[] items;


    void OnEnable () {
        foreach (Item item in items) {
            item.BeenPicked += OnItemBePicked;
        }
    }

    void OnDisable () {
        foreach (Item item in items) {
            item.BeenPicked -= OnItemBePicked;
        }
    }

    void OnItemBePicked () {
        foreach (Item item in items) {
            item.canBePick = false;
        }
    }

}