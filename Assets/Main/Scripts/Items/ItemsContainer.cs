using System;
using System.Linq;
using UnityEngine;
using DoubleHeat.Common;

public class ItemsContainer : MonoBehaviour {
    
    public Transform itemSpawnPoint;
    public BoundsArea boundsArea;


    public void AddItem (Item item) {   

        item.transform.position = itemSpawnPoint.position;
        item.transform.rotation = UnityEngine.Random.rotation;

    }


    public bool ContainsItem (Item item) {
        return boundsArea.bounds.Contains(item.transform.position);
    }

    public bool ContainsItemWithTag (string tag) {
        return Item.GetItemsWithTag(tag).Any(item => ContainsItem(item));
    }

}