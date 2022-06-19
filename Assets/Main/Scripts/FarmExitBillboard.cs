using DG.Tweening;
using UnityEngine;

public class FarmExitBillboard : MonoBehaviour {
    
    public float rotCamDuration = 0.8f;
    public Ease rotCamEase;


    private void OnEnable() {
        GlobalEventManager.AddListener("exit farm", OnExitFarm);
    }
    
    private void OnDisable() {
        GlobalEventManager.RemoveListener("exit farm", OnExitFarm);
    }


    void OnExitFarm () {
        var carCam = FindObjectOfType<CarCamera>();

        if (carCam) {
            carCam.enabled = false;

            // Vector3 dir = (this.transform.position - carCam.transform.position).normalized;
            carCam.transform.DOLookAt(this.transform.position, rotCamDuration)
                .SetEase(rotCamEase);
        }
    }
}