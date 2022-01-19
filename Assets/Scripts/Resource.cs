using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Resource : MonoBehaviour {

    public ResourceType resourceType;
    
    Collider collider;

    void Awake() {
        collider = GetComponent<Collider>();
    }
}

public enum ResourceType {
    Wood,
    Stone
}