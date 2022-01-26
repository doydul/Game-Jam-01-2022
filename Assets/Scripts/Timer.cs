using UnityEngine;

public class Timer {

    float duration;
    float start;
    
    public Timer(float duration) {
        this.duration = duration;
        Start();
    }

    public void Start() {
        start = Time.time;
    }

    public bool Check() {
        if (Time.time - start >= duration) {
            Start();
            return true;
        } else {
            return false;
        }
    }
}