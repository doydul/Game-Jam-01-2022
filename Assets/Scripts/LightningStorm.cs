using UnityEngine;

public class LightningStorm : MonoBehaviour {
    
    public float timeBetweenStrikes;
    public float stormDuration;
    public float hitPlayerChance;
    public float stormChance;
    public GameObject lightningPrefab;

    Timer stormTimer;
    Timer strikeTimer;
    HeroMovement hero;

    public Transform lightningRod { get; set; }

    void Awake() {
        hero = FindObjectOfType<HeroMovement>();
        strikeTimer = new Timer(timeBetweenStrikes);
    }

    void Update() {
        if (stormTimer != null) {
            if (stormTimer.Check()) {
                StopStorm();
            } else if (strikeTimer.Check()) {
                var lb = Instantiate(lightningPrefab);
                if (Random.value < hitPlayerChance) {
                    if (lightningRod != null) {
                        lb.transform.position = lightningRod.position;
                    } else {
                        lb.transform.position = hero.transform.position;
                    }
                } else {
                    lb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                        Random.value * Camera.main.pixelWidth,
                        Random.value * Camera.main.pixelHeight,
                        10
                    ));
                }
            }
        }
    }

    public void StartDayNightCycle() {
        if (Random.value < stormChance) {
            StartStorm();
        }
    }

    public void StartStorm() {
        stormTimer = new Timer(stormDuration);
    }

    public void StopStorm() {
        stormTimer = null;
    }
}