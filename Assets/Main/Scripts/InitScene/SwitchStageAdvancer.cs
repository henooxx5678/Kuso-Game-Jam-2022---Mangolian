using System;
using UnityEngine;

public class SwitchStageAdvancer : StageAdvacner {
    
    public GameObject[] disableObjects;
    public GameObject[] enableObjects;

    protected override void OnPlayerAdvance () {

        Array.ForEach(disableObjects, obj => obj.SetActive(false));
        Array.ForEach(enableObjects, obj => obj.SetActive(true));

    }

}