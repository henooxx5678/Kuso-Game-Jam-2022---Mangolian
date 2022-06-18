using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Item : RigidbodyAttached {

    public static List<Item> instances = new List<Item>();

    public static IEnumerable<Item> GetItemsWithTag (string tag) {
        return instances.Where(item => item.tags.Contains(tag));
    }




    public string[] tags;
    
    
    void Awake () {
        instances.Add(this);
    }

    void OnDestroy() {
        instances.Remove(this);
    }


    public bool IsInPickableRange () {
        return (this.transform.position - PlayerCar.current.transform.position).sqrMagnitude < PlayerCar.pickRangeDistance * PlayerCar.pickRangeDistance;
    }



}