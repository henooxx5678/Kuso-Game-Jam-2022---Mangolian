using UnityEngine;

public class PathHolder : MonoBehaviour {
    
    public static PathHolder current;


    Transform[] _pathPoints;
    public Transform[] pathPoints {
        get {
            if (_pathPoints == null) {
                _pathPoints = new Transform[this.transform.childCount];
                for (int i = 0 ; i < _pathPoints.Length ; i++) {
                    _pathPoints[i] = this.transform.GetChild(i);
                }
            }
            return _pathPoints;
        }
    }


    void Awake () {
        current = this;
    }


    public void GetPoseOfResetToPathPoint (Vector3 pos, out Vector3 position, out Quaternion rotation) {

        if (pathPoints.Length > 0) {
            float min = Mathf.Infinity;
            Transform targetPoint = pathPoints[0];

            foreach (Transform point in pathPoints) {
                Vector3 displacement = pos - point.position;
                float dist = Vector3.Dot(displacement, point.forward) > Mathf.Epsilon ? displacement.magnitude : Mathf.Infinity;
                if (dist >= 0 && dist < min) {
                    min = dist;
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