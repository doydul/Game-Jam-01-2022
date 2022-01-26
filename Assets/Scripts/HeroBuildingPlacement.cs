using UnityEngine;

public class HeroBuildingPlacement : MonoBehaviour {

    public GameObject buildingPrefab;
    
    GameManager manager;

    Building currentBuilding;

    public bool active => currentBuilding != null;

    void Awake() {
        manager = FindObjectOfType<GameManager>();
    }

    void Update() {
        if (active) {
            currentBuilding.GetComponent<Rigidbody2D>().MovePosition(transform.position);
        }
    }

    public void StartPlacing(CraftableItem item) {
        var building = Instantiate(buildingPrefab);
        currentBuilding = building.GetComponent<Building>();
        currentBuilding.GetComponent<Rigidbody2D>().MovePosition(transform.position);
        currentBuilding.Init(item.scale, item.sprite, item.name);
    }

    public void Place() {
        if (currentBuilding.canPlace) {
            currentBuilding.Place();
            manager.builtBuildings.Add(currentBuilding.name);
            currentBuilding = null;
        }
    }

    public void Cancel() {
        if (currentBuilding == null) return;
        Destroy(currentBuilding.gameObject);
        currentBuilding = null;
    }
}