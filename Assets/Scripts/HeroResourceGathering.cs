using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class HeroResourceGathering : MonoBehaviour {

    public float miningInterval;

    GameManager gameManager;
    Collider collider;

    Resource activeResource;
    bool mining;
    float miningStart;
    List<Building> buildings;

    void Awake() {
        collider = GetComponent<Collider>();
        gameManager = FindObjectOfType<GameManager>();
        buildings = new List<Building>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        var resource = other.GetComponent<Resource>();
        if (resource != null) {
            activeResource = resource;
        }
        var buildingCollider = other.GetComponent<BuildingCollider>();
        if (buildingCollider != null) {
            buildings.Add(buildingCollider.building);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        var resource = other.GetComponent<Resource>();
        if (resource != null && resource == activeResource) {
            activeResource = null;
        }
        var buildingCollider = other.GetComponent<BuildingCollider>();
        if (buildingCollider != null) {
            buildings.Remove(buildingCollider.building);
        }
    }

    void Update() {
        if (activeResource != null && mining && Time.time - miningStart >= miningInterval) {
            miningStart = Time.time;
            if (activeResource.resourceType == ResourceType.Stone) {
                gameManager.AddStone(1);
            } else {
                gameManager.AddWood(1);
            }
        }
    }

    public void StartMining() {
        miningStart = Time.time;
        mining = true;
    }

    public void StopMining() {
        mining = false;
    }

    public void Interact() {
        foreach (var building in buildings) {
            building.Interact();
        }
    }
}