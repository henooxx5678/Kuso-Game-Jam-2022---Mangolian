using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Item : RigidbodyAttached {

    public static List<Item> instances = new List<Item>();

    public static IEnumerable<Item> GetItemsWithTag (string tag) {
        return instances.Where(item => item.tags.Contains(tag));
    }

    public static event Action<Item> ItemBeenPicked;


    public event Action BeenPicked;

    public bool canBePick = true;
    public string[] tags;
    
    
    void Awake () {
        instances.Add(this);
    }

    void OnDestroy() {
        instances.Remove(this);
    }

    void OnMouseDown () {
        TryBeenPick();
    }


    public bool IsInPickableRange () {
        return (this.transform.position - PlayerCar.current.transform.position).sqrMagnitude < PlayerCar.pickRangeDistance * PlayerCar.pickRangeDistance;
    }


    public void TryBeenPick () {
        if (IsInPickableRange()) {

            if (canBePick) {
                BeenPicked?.Invoke();
                ItemBeenPicked?.Invoke(this);
            }
            else {
                GlobalEventManager.TriggerEvent("try to pick multiple girls");
                print("kuso DD");
            }


        }
        else {
            GlobalEventManager.TriggerEvent("too far to pick");
            print("too far");
        }
    }


}