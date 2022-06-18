using System.Collections.Generic;
using UnityEngine;

public class PoliceCarBehaviour : RigidbodyAttached {
    
    public static List<PoliceCarBehaviour> instances = new List<PoliceCarBehaviour>();

    public static void SetIsHuntingOfAllInstances (bool isHunting) {
        foreach (var instance in instances) {
            instance.isHunting = isHunting;
        }
    }

    




    public bool isHunting = false;

    public float targetSpeed = 10f;
    public float toTargetVelocityTime = 2f;
    public float maxAcceleration = 5f;

    public float maxAngSpeed = 10f;

    public float velocitySqrMagnitudeThreshold = 0.0001f;

    protected PlayerCar playerCar => PlayerCar.current;


    void Awake () {
        instances.Add(this);
    }

    void OnDestroy () {
        instances.Remove(this);
    }

    void FixedUpdate () {

        if (isHunting && playerCar) {
            
            Vector3 targetVelocity = (playerCar.transform.position - this.transform.position).normalized * targetSpeed;

            Vector3 acc = (targetVelocity - rb.velocity) / toTargetVelocityTime;
            acc = acc.normalized * Mathf.Min(acc.magnitude, maxAcceleration);

            rb.AddForce(acc * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else {
            if (rb.velocity.sqrMagnitude > velocitySqrMagnitudeThreshold) {
                Vector3 dellaVelocity = -rb.velocity.normalized;
                rb.AddForce(dellaVelocity.normalized * Mathf.Min(maxAcceleration * Time.fixedDeltaTime, dellaVelocity.magnitude), ForceMode.VelocityChange);
            }
        }


        // auto facing forward
        if (rb.velocity.sqrMagnitude > velocitySqrMagnitudeThreshold) {
            float angle = Vector3.Angle(this.transform.forward, rb.velocity.normalized);
            Vector3 axis = Vector3.Cross(this.transform.forward, rb.velocity).normalized;
            axis = Mathf.Sign(Vector3.Dot(Vector3.up, axis)) * Vector3.up;

            rb.MoveRotation(Quaternion.AngleAxis(Mathf.Min(angle, maxAngSpeed * Time.fixedDeltaTime), axis) * rb.rotation);
        }
    }

}