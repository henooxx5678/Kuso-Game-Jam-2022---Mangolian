using UnityEngine;

public class CarSelectedDetector : MonoBehaviour {
    
    public GameObject thisStageObj;
    public GameObject nextStageObj;


    void OnEnable () {
        GlobalEventManager.AddListener("car selected", OnCarSelected);
    }


    void OnDisable () {
        GlobalEventManager.RemoveListener("car selected", OnCarSelected);
    }

    void OnCarSelected () {

        thisStageObj.SetActive(false);
        nextStageObj.SetActive(true);

    }
}