using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public GameObject resourceCountersObject;
    public TMP_Text woodCounter;
    public TMP_Text stoneCounter;
    public Slider healthSlider;
    public Slider hungerSlider;
    public GameObject nightShade;
    public GameObject youWin;
    public GameObject youLose;
    public GameObject ui;
    public float nightDayDuration;
    public float nightDamageDuration;
    public int nightDamageAmount;
    public int hungerDamageAmount;
    public int hungerDeteriation;

    BuildingMenu buildingMenu;
    HeroMovement hero;
    HeroBuildingPlacement heroBuilding;
    DayNightIndicator dayNight;
    LightningStorm storm;
    BearAttack bears;

    int woodCount;
    int stoneCount;
    int health;
    int hunger;
    float dayNightStart;
    Timer nightDamageTimer;
    Timer hungerDamageTimer;
    Timer hungerTimer;
    Timer endScreenTimer;

    public bool isNight { get; private set; }
    public List<string> builtBuildings { get; private set; }

    void Awake() {
        buildingMenu = FindObjectOfType<BuildingMenu>();
        hero = FindObjectOfType<HeroMovement>();
        heroBuilding = FindObjectOfType<HeroBuildingPlacement>();
        dayNight = FindObjectOfType<DayNightIndicator>();
        storm = FindObjectOfType<LightningStorm>();
        bears = FindObjectOfType<BearAttack>();
        builtBuildings = new List<string>();
        health = 100;
        hunger = 100;
        dayNightStart = Time.time;
        hungerTimer = new Timer(nightDamageDuration);
        youLose.SetActive(false);
        youWin.SetActive(false);
    }

    void Update() {
        DayNightCycle();
        Hunger();
        if (Input.GetKeyDown(KeyCode.C)) {
            heroBuilding.Cancel();
            if (buildingMenu.isOpen) {
                buildingMenu.Close();
                resourceCountersObject.SetActive(true);
                hero.EnableInput();
            } else {
                hero.DisableInput();
                buildingMenu.Open();
                resourceCountersObject.SetActive(false);
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
            LoseGame();
        }
        if (endScreenTimer != null && endScreenTimer.Check()) {
            SceneManager.LoadScene("menu");
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
                storm.StartDayNightCycle();
                if (Random.value < 0.7f) {
                    bears.StartBearAttack();
                }
            } else {
                dayNight.Night();
                isNight = true;
                nightShade.SetActive(true);
                nightDamageTimer = new Timer(nightDamageDuration);
            }
            storm.StartDayNightCycle();
        }
        if (nightDamageTimer != null && nightDamageTimer.Check()) {
            AddHealth(-nightDamageAmount);
        }
    }

    void Hunger() {
        if (hunger <= 0 && hungerDamageTimer == null) {
            hungerDamageTimer = new Timer(nightDamageDuration);
        } else if (hunger > 0) {
            hungerDamageTimer = null;
        }
        if (hungerDamageTimer != null && hungerDamageTimer.Check()) {
            AddHealth(-hungerDamageAmount);
        }
        if (hungerTimer.Check()) {
            AddHunger(-hungerDeteriation);
        }
    }

    public void WinGame() {
        youWin.SetActive(true);
        ui.SetActive(false);
        endScreenTimer = new Timer(5);
        Destroy(hero.gameObject);
    }

    public void LoseGame() {
        if (hero == null) return;
        youLose.SetActive(true);
        ui.SetActive(false);
        endScreenTimer = new Timer(5);
        Destroy(hero.gameObject);
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
    
    public void AddHunger(int amount) {
        hunger += amount;
        if (hunger > 100) hunger = 100;
        if (hunger < 0) hunger = 0;
        hungerSlider.value = (float)hunger / 100;
    }
}