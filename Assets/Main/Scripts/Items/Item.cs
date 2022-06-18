using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Item : RigidbodyAttached {

    public static List<Item> instances = new List<Item>();

    public static IEnumerable<Item> GetItemsWithTag (string tag) {
        return instances.Where(item => item.tags.Contains(tag));
    }

    public static event Action<Item> ItemPicked;



    public string[] tags;
    
    
    void Awake () {
        instances.Add(this);
    }

    void OnDestroy() {
        instances.Remove(this);
    }

    void OnMouseDown () {
        TryBeenPick();
        print(1);
    }


    public bool IsInPickableRange () {
        return (this.transform.position - PlayerCar.current.transform.position).sqrMagnitude < PlayerCar.pickRangeDistance * PlayerCar.pickRangeDistance;
    }


    public void TryBeenPick () {
        if (IsInPickableRange()) {
            ItemPicked?.Invoke(this);
            print(1.2f);
        }
        else {
            print("too far");
        }
    }


}