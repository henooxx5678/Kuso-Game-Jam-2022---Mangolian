using UnityEngine;

public class HappyValueHandler : MonoBehaviour {
    
    public float happyValue = 100f;
    public float happyIncreaseRate = 0.1f;
    public float collisionRelSpeedThreshold = 5f;
    public float happyDecreasePerCollision = 99f;
    public float unhappyCooldown = 1.5f;

    [Header("condifions")]
    public float buttonValue;
    public float topValue;


    protected float currentSpeed => PlayerCar.current.carController.speed;


    float _prevUnhappyTime = 0f;
    bool _hasEnd = false;


    void OnEnable () {
        PlayerCar.Collided += OnPlayerCarCollide;
    }

    void OnDisable () {
        PlayerCar.Collided -= OnPlayerCarCollide;
    }
    
    void Update () {
        
        if (_hasEnd)
            return;

        happyValue += currentSpeed * happyIncreaseRate;

        if (happyValue > topValue) {
            GlobalEventManager.TriggerEvent("happy to win");
            _hasEnd = true;
        }

        if (happyValue < buttonValue) {
            GlobalEventManager.TriggerEvent("unhappy to lose");
            _hasEnd = true;
        }


    }

    void OnPlayerCarCollide (float relSpeed) {
        if (Time.time - _prevUnhappyTime > unhappyCooldown) {
            if (relSpeed > collisionRelSpeedThreshold) {
                happyValue -= happyDecreasePerCollision;
                _prevUnhappyTime = Time.time;
            }
        }
    }

}