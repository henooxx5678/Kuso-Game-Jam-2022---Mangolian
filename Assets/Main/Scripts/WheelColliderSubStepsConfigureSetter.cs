using UnityEngine;

public class WheelColliderSubStepsConfigureSetter : MonoBehaviour {
    
    public float speedThreshold;
    public int stepsBelowThreshold;
    public int stepsAboveThreshold;

    void Start () {

        this.GetComponent<WheelCollider>().ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);

    }

}