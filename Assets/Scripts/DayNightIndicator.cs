using UnityEngine;

public class DayNightIndicator : MonoBehaviour {
    
    public GameObject day;
    public GameObject night;

    void Awake() {
        Day();
    }

    public void Day() {
        day.SetActive(true);
        night.SetActive(false);
    }

    public void Night() {
        day.SetActive(false);
        night.SetActive(true);
    }
}