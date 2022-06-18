using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour {
    
    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;

    void OnMouseDown() {
        onMouseDown?.Invoke();
    }

    void OnMouseUp () {
        onMouseUp?.Invoke();
    }

}