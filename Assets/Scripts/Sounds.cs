using UnityEngine;

public class Sounds : MonoBehaviour {

    public static Sounds instance;
    
    public AudioClip thunder1;
    public AudioClip thunder2;
    public AudioSource bearSource;
    public AudioSource thunderSource;

    void Awake() {
        instance = this;
    }

    public void BearSound() {
        bearSource.Play();
    }

    public void ThunderSound() {
        bool first = Random.value < 0.5f;
        if (first) {
            thunderSource.clip = thunder1;
        } else {
            thunderSource.clip = thunder2;
        }
        thunderSource.Play();
    }
}