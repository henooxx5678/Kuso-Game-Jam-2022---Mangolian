using UnityEngine;

public class PathHolder : MonoBehaviour {
    
    public Transform[] pathPoints;


    public void GetPoseOfResetToPathPoint (Vector3 pos, out Vector3 position, out Quaternion rotation) {

        if (pathPoints.Length > 0) {
            float min = Mathf.NegativeInfinity;
            Transform targetPoint = pathPoints[0];

            foreach (Transform point in pathPoints) {
                float thisValue = Vector3.Dot(pos - point.position, point.forward);
                if (thisValue >= 0 && thisValue < min) {
                    min = thisValue;
                    targetPoint = point;
                }
            }

            position = targetPoint.position;
            rotation = targetPoint.rotation;
        }
        else {
            position = Vector3.zero;
            rotation = Quaternion.identity;
        }

    }

}