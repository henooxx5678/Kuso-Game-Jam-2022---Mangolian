using UnityEngine;
using DoubleHeat.Common;


public class PlayerCar : MonoBehaviour {

    public static PlayerCar current;

    public static float pickRangeDistance = 5f;


    CarControler _carController;
    public CarControler carController => _carController ? _carController : _carController = this.GetComponent<CarControler>();

    ItemsContainer _itemsContainer;
    public ItemsContainer itemsContainer => _itemsContainer ? _itemsContainer : _itemsContainer = this.GetComponentInChildren<ItemsContainer>();



    void Awake () {
        if (current) {
            Destroy(current);
        }
        current = this;
    }


    void OnEnable () {
        carController.isRecievingPlayerInput = true;
        
        Item.ItemPicked += OnItemPicked;
    }

    void OnDisable () {
        carController.isRecievingPlayerInput = false;

        Item.ItemPicked -= OnItemPicked;
    }
    
    void FixedUpdate () {
        CheckContainedItems();
    }

    public void CheckContainedItems () {
        if (itemsContainer) {

            // driver license
            PoliceCarBehaviour.SetIsHuntingOfAllInstances(!itemsContainer.ContainsItemWithTag("driver license"));

            // alcohol
            carController.isDrunk = itemsContainer.ContainsItemWithTag("alcohol");
        }
    }


    protected void OnItemPicked (Item item) {
        itemsContainer.AddItem(item);
        print(2);
    }


}