using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StonePile : MonoBehaviour {
    
    Collider collider;

    void Awake() {
        collider = GetComponent<Collider>();
    }
}