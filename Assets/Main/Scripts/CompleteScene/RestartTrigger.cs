using UnityEngine;

public class RestartTrigger : MonoBehaviour {
    
    void OnTriggerEnter (Collider other) {
        TerrainScenesManager.current.RestartGame();
    }

}