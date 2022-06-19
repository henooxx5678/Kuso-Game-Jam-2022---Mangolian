using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DoubleHeat.Common;

/// <summary>
/// Car Controls
/// </summary>
public class CarControler : MonoBehaviour
{

    public bool isRecievingPlayerInput;


    public bool isDrunk = false;
    public FloatRange drunkHorizontalInputRange = new FloatRange(0f, 0.75f, true);
    public FloatRange drunkVerticalInputRange = new FloatRange(0f, 1.5f, true);
    public float drunkMovePeriod = 0.3f;
    public float drunkMoveIntervalTime = 0.5f;

    /// <summary>
    /// List of the wheel settings of the car.
    /// </summary>
    public List<WheelAxle> wheelAxleList;

    /// <summary>
    /// Car settings of the car.
    /// </summary>
    public CarSettings carSettings;
  
    /// <summary>
    /// Rigidbody of the car.
    /// </summary>
    private Rigidbody rbody;

    /// <summary>
    /// Calculated speed of the car.
    /// </summary>
    public float speed = 0;



    float _currentWaitForNextDrunkMoveTime = 0f;
    DrunkState _currentDrunkState = null;



    bool _isStartListeningInput = false;

    

    protected class DrunkState {
        public float startTime;
        public float horizontalInput;
        public float verticalInput;

        public DrunkState (float time, FloatRange verticalInputRange) {
            startTime = time;
            horizontalInput = Random.Range(-1f, 1f);
            verticalInput = Random.Range(verticalInputRange.min, verticalInputRange.max);
        }
    }




    private void Awake()
    {
        ///create rigidbody
        rbody = this.GetComponent<Rigidbody>();

        ///set mass of the car
        rbody.mass = carSettings.mass;

        ///set drag of the car
        rbody.drag = carSettings.drag;

        //set the center of mass of the car
        rbody.centerOfMass = carSettings.centerOfMass;
    }


   /// <summary>
   /// Visual Transformation of the car wheels.
   /// </summary>
   /// <param name="wheelCollider"></param>
   /// <param name="wheelMesh"></param>
    public void ApplyWheelVisuals(WheelCollider wheelCollider, GameObject wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;

        ///get position and rotation of the WheelCollider
        wheelCollider.GetWorldPose(out position, out rotation);
        
        ///calculate real rotation of the wheels
        Quaternion realRotation = rotation * Quaternion.Inverse(wheelCollider.transform.parent.rotation) * this.transform.rotation;
       
        ///set position of the wheel
        wheelMesh.transform.position = position;
        
        ///set rotation of the wheel
        wheelMesh.transform.rotation = realRotation;
    }

    public void ResetVelocity () {
        rbody.velocity = Vector3.zero;
    }



    void OnEnable () {
        _isStartListeningInput = false;
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

    
        if (!isRecievingPlayerInput) {
            verticalInput = 0f;
            horizontalInput = 0f;
        }


        if (!_isStartListeningInput) {
            if (verticalInput < 0.5f) {
                _isStartListeningInput = true;
            }
            else {
                verticalInput = 0f;
                horizontalInput = 0f;
            }
        }


        if (isDrunk) {

            // update drunk state
            _currentWaitForNextDrunkMoveTime += Time.fixedDeltaTime;

            if (_currentWaitForNextDrunkMoveTime > drunkMoveIntervalTime) {

                _currentDrunkState = new DrunkState(Time.time, drunkVerticalInputRange);

                _currentWaitForNextDrunkMoveTime -= drunkMoveIntervalTime;
            }

            if (_currentDrunkState != null) {
                if (Time.time - _currentDrunkState.startTime > drunkMovePeriod) {
                    _currentDrunkState = null;
                }
            }


            // apply drunk move
            if (_currentDrunkState != null) {
                verticalInput = _currentDrunkState.verticalInput;
                horizontalInput = _currentDrunkState.horizontalInput;
            }
        }

        

        ///get speed of the car
        speed = rbody.velocity.magnitude;


        bool isMovingForward = Vector3.Dot(rbody.velocity, this.transform.forward) > 0;


        ///calculate motor torque
        float motor = (isMovingForward ? Mathf.Max(verticalInput, 0f) : Mathf.Min(verticalInput, 0f)) * carSettings.motorTorque;
        float pedalBrake = Mathf.Abs(isMovingForward ? Mathf.Min(verticalInput, 0f) : Mathf.Max(verticalInput, 0f)) * carSettings.padelBrakeTorque;

        //calculate wheel steering
        float steering = carSettings.steeringAngle * horizontalInput;

        ///calculate motor break
        float handBrake = Input.GetKey(KeyCode.Space) == true ? carSettings.handBrakeTorque * 1 : 0;
        
        ///iterate all wheel axles
        foreach (WheelAxle wheelAxle in wheelAxleList)
        {

            ///this is a steering axle
            if (wheelAxle.steering)
            {
                ///apply steering
                wheelAxle.wheelColliderLeft.steerAngle = steering;
                wheelAxle.wheelColliderRight.steerAngle = steering;
            }

            ///this is motor axle
            if (wheelAxle.motor)
            {
                ///apply motor torque
                wheelAxle.wheelColliderLeft.motorTorque = motor;
                wheelAxle.wheelColliderRight.motorTorque = motor;
            }



            ///apply wheel visuals
            ApplyWheelVisuals(wheelAxle.wheelColliderLeft, wheelAxle.wheelMeshLeft);
            ApplyWheelVisuals(wheelAxle.wheelColliderRight, wheelAxle.wheelMeshRight);
        }

        wheelAxleList[0].wheelColliderLeft.brakeTorque = pedalBrake;
        wheelAxleList[0].wheelColliderRight.brakeTorque = pedalBrake;
        
        ///apply hand break
        wheelAxleList[1].wheelColliderLeft.brakeTorque = handBrake;
        wheelAxleList[1].wheelColliderRight.brakeTorque = handBrake;
    }
}