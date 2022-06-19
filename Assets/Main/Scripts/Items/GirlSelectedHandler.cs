using UnityEngine;

public class GirlSelectedHandler : MonoBehaviour {
    
    public int girlNumber;


    Item _item;
    protected Item item => _item ? _item : _item = this.GetComponent<Item>();


    void OnEnable () {
        item.BeenPicked += OnPicked;
    }
    
    void OnDisable () {
        item.BeenPicked -= OnPicked;
    }

    void OnPicked () {
        SelectionsManager.girlNumber = girlNumber;
    }

}