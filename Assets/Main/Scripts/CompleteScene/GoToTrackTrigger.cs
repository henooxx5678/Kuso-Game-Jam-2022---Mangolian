using UnityEngine;

public class GoToTrackTrigger : MonoBehaviour {
    
    void OnTriggerEnter (Collider other) {
        TerrainScenesManager.current.LoadScene("Terrain Free Track");
    }

}