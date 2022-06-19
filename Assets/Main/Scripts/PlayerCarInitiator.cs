using UnityEngine;

public class PlayerCarInitiator : MonoBehaviour {

    public GameObject defaultCarPrefab;
    public GameObject[] girlPrefabs;

    public Transform startPoint;

    void OnEnable () {

        if (!PlayerCar.current) {

            GameObject carPrefab = SelectionsManager.carPrefab ? SelectionsManager.carPrefab : defaultCarPrefab;

            if (carPrefab) {
                GameObject carObj = Instantiate(carPrefab);
                carObj.AddComponent<PlayerCar>();
            }
            else {
                print("no car prefab");
                return;
            }

        }

        PlayerCar.current.SetPose(startPoint.position, startPoint.rotation);

        int girlNum = SelectionsManager.girlNumber;

        if (girlNum >= 0 && girlNum < girlPrefabs.Length) {
            GameObject girlObj = Instantiate(girlPrefabs[girlNum]);
            PlayerCar.current.itemsContainer.AddItem(girlObj.GetComponent<Item>());
        }

    }

}