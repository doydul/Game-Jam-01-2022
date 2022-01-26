using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BuildingCollider : MonoBehaviour {
    
    public Building building;

    Collider2D collider;

    int overlaps;

    void Awake() {
        collider = GetComponent<Collider2D>();
        building.CanPlace();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.isTrigger) return;
        overlaps++;
        building.CantPlace();
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.isTrigger) return;
        overlaps--;
        if (overlaps <= 0) building.CanPlace();
    }
}