using System;
using System.Linq;
using UnityEngine;
using DoubleHeat.Common;


public class PlayerCar : MonoBehaviour {

    public static PlayerCar current;

    public static float pickRangeDistance = 7f;


    public float huntedSpeedThreshold = 0.3f;
    public KeyCode restPoseKey => KeyCode.R;


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


        CarCamera carCam = UnityEngine.Object.FindObjectOfType<CarCamera>();
        carCam.gameObject.SetActive(true);
        carCam.target = this.transform;
    }

    void OnDisable () {
        carController.isRecievingPlayerInput = false;

        Item.ItemPicked -= OnItemPicked;
    }
    
    void FixedUpdate () {
        CheckContainedItems();
    }

    void Update () {
        if (Input.GetKeyDown(restPoseKey)) {
            ResetPose();
        }
    }


    public void CheckContainedItems () {
        if (itemsContainer) {

            // driver license
            PoliceCarBehaviour.SetIsHuntingOfAllInstances(!itemsContainer.ContainsItemWithTag("driver license") && carController.speed > huntedSpeedThreshold);

            // alcohol
            carController.isDrunk = itemsContainer.ContainsItemWithTag("alcohol");
        }
    }


    protected void OnItemPicked (Item item) {
        itemsContainer.AddItem(item);
    }


    protected void ResetPose () {

        if (PathHolder.current) {
            Vector3 pos;
            Quaternion rot;
            PathHolder.current.GetPoseOfResetToPathPoint(this.transform.position, out pos, out rot);

            Vector3 deltaPos = pos - this.transform.position;
            Quaternion deltaRot = rot * Quaternion.Inverse(this.transform.rotation);



            GameObject parentObject = new GameObject("player car parent");
            parentObject.transform.position = this.transform.position;

            this.transform.SetParent(parentObject.transform, true);

            itemsContainer.GetContainedItems().ToList().ForEach(item => item.transform.SetParent(parentObject.transform, true));

            parentObject.transform.position += deltaPos;
            parentObject.transform.rotation = deltaRot * parentObject.transform.rotation;
            
            this.transform.SetParent(null, true);
            itemsContainer.GetContainedItems().ToList().ForEach(item => item.transform.SetParent(null, true));
            Destroy(parentObject);
        }
        else {
            print("no path holder");
        }
    }

}