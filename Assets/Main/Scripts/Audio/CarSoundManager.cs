using UnityEngine;
using DoubleHeat.Common;

public class CarSoundManager : MonoBehaviour {

    [Header("Engine")]    
    public AudioSource engineSrc;
    public AudioClip idleClip;
    public AudioClip runClip;
    public float idleRunSpeedThreshold = 0.3f;
    public float speedForOriginalPitch = 10f;
    public float pitchChangeRate = 0.01f;

    [Header("Collide")]
    public float relSpeedThreshold = 5f;
    public AudioSource collisionSrc;
    public FloatRange pitchRandomRange = new FloatRange(1f, 1f);


    void OnEnable () {
        PlayerCar.Collided += OnPlayerCarCollide;
    }

    void OnDisable () {
        PlayerCar.Collided -= OnPlayerCarCollide;
    }

    void Update () {
        if (PlayerCar.current) {

            float speed = PlayerCar.current.carController.speed;

            if (speed > idleRunSpeedThreshold) {
                engineSrc.clip = runClip;
                
                engineSrc.pitch = GetPitchBySpeed(speed);
            }
            else {
                engineSrc.clip = idleClip;
                engineSrc.pitch = 0.9f;
            }

            if (!engineSrc.isPlaying) {
                engineSrc.Play();
            }
        }
        else {
            engineSrc.Stop();
        }
    }

    void OnPlayerCarCollide (float relSpeed) {
        if (relSpeed > relSpeedThreshold) {
            collisionSrc.pitch = Random.Range(pitchRandomRange.min, pitchRandomRange.max);
            if (!collisionSrc.isPlaying)
                collisionSrc.Play();
        }
        // print("rel speed: " + relSpeed);
    }

    
    float GetPitchBySpeed (float speed) {
        return 1f + (speed - 10f) * pitchChangeRate;
    }

}