using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAdvanceHandler : MonoBehaviour {
    
    public int requiredEnterCount = 7;
    public float resetDuration = 1f;
    public KeyCode key;

    int _counter = 0;
    float _prevEnterTime = 0f;


    void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update () {
        if (Input.GetKeyDown(key)) {
            _counter++;
            _prevEnterTime = Time.realtimeSinceStartup;
        }

        if (_counter >= requiredEnterCount) {
            GlobalEventManager.TriggerEvent("player advance");
            _counter = 0;
            print("ADV");
        }


        if (Time.realtimeSinceStartup - _prevEnterTime > resetDuration) {
            _counter = 0;
        }

    }

}