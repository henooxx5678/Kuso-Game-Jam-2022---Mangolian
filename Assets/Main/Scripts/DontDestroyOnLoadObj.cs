using UnityEngine;

public class DontDestroyOnLoadObj : MonoBehaviour {
    
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }

}