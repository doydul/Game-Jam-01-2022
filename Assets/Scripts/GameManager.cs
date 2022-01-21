using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    
    public GameObject resourceCountersObject;
    public TMP_Text woodCounter;
    public TMP_Text stoneCounter;

    BuildingMenu buildingMenu;
    HeroMovement hero;
    HeroBuildingPlacement heroBuilding;

    int woodCount;
    int stoneCount;

    void Awake() {
        buildingMenu = FindObjectOfType<BuildingMenu>();
        hero = FindObjectOfType<HeroMovement>();
        heroBuilding = FindObjectOfType<HeroBuildingPlacement>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (buildingMenu.isOpen) {
                buildingMenu.Close();
                hero.EnableInput();
                resourceCountersObject.SetActive(true);
            } else {
                hero.DisableInput();
                buildingMenu.Open();
                resourceCountersObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            if (buildingMenu.isOpen) {
                heroBuilding.StartPlacing(buildingMenu.GetSelection());
                buildingMenu.Close();
                hero.EnableInput();
                resourceCountersObject.SetActive(true);
            }
        }
    }

    public void AddWood(int amount) {
        woodCount += amount;
        woodCounter.text = woodCount.ToString();
    }

    public void AddStone(int amount) {
        stoneCount += amount;
        stoneCounter.text = stoneCount.ToString();
    }
}