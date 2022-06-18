using UnityEngine;

public class RigidbodyAttached : MonoBehaviour {

    Rigidbody _rb;
    public Rigidbody rb => _rb ? _rb : _rb = this.GetComponent<Rigidbody>();
    
}