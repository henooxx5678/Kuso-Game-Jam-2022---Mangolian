using UnityEngine;

public class GirlInitiator : MonoBehaviour {
    
    public GameObject[] girls;


    void Start () {
        for (int i = 0 ; i < girls.Length ; i++) {
            girls[i].SetActive(i == SelectionsManager.girlNumber);
        }
    }

}