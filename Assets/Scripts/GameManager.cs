using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    
    public GameObject resourceCountersObject;
    public TMP_Text woodCounter;
    public TMP_Text stoneCounter;
    public Slider healthSlider;
    public GameObject nightShade;
    public float nightDayDuration;
    public float nightDamageDuration;
    public int nightDamageAmount;

    BuildingMenu buildingMenu;
    HeroMovement hero;
    HeroBuildingPlacement heroBuilding;
    DayNightIndicator dayNight;

    int woodCount;
    int stoneCount;
    int health;
    float dayNightStart;
    Timer nightDamageTimer;

    public bool isNight { get; private set; }
    public List<string> builtBuildings { get; private set; }

    void Awake() {
        buildingMenu = FindObjectOfType<BuildingMenu>();
        hero = FindObjectOfType<HeroMovement>();
        heroBuilding = FindObjectOfType<HeroBuildingPlacement>();
        dayNight = FindObjectOfType<DayNightIndicator>();
        builtBuildings = new List<string>();
        health = 100;
        dayNightStart = Time.time;
    }

    void Update() {
        DayNightCycle();
        if (Input.GetKeyDown(KeyCode.C)) {
            if (buildingMenu.isOpen) {
                buildingMenu.Close();
                resourceCountersObject.SetActive(false);
                hero.EnableInput();
                heroBuilding.Cancel();
            } else {
                hero.DisableInput();
                buildingMenu.Open();
                resourceCountersObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            if (buildingMenu.isOpen) {
                var item = buildingMenu.GetSelection();
                if (woodCount >= item.woodRequirement && stoneCount >= item.stoneRequirement && item.dependencies.All(ci => builtBuildings.Contains(ci.name)) && builtBuildings.Count(b => b == item.name) < item.limit) {
                    AddWood(-item.woodRequirement);
                    AddStone(-item.stoneRequirement);
                    heroBuilding.StartPlacing(item);
                    buildingMenu.Close();
                    resourceCountersObject.SetActive(true);
                    hero.EnableInput();
                } else {
                    // play a noise or something
                    Debug.Log("Can't build that lol");
                }
            }
        }
        if (health <= 0) {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    void DayNightCycle() {
        if (Time.time - dayNightStart >= nightDayDuration) {
            dayNightStart = Time.time;
            if (isNight) {
                dayNight.Day();
                isNight = false;
                nightShade.SetActive(false);
                nightDamageTimer = null;
            } else {
                dayNight.Night();
                isNight = true;
                nightShade.SetActive(true);
                nightDamageTimer = new Timer(nightDamageDuration);
            }
        }
        if (nightDamageTimer != null && nightDamageTimer.Check()) {
            AddHealth(-nightDamageAmount);
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

    public void AddHealth(int amount) {
        health += amount;
        if (health > 100) health = 100;
        healthSlider.value = (float)health / 100;
    }
}