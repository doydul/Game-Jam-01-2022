using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightningBolt : MonoBehaviour {

    public float lifespan;
    public int damage;
    
    Collider2D collider;
    Timer life;

    void Awake() {
        collider = GetComponent<Collider2D>();
        life = new Timer(lifespan);
    }

    void Update() {
        if (life.Check()) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        var hero = other.GetComponent<HeroMovement>();
        if (hero != null) {
            Debug.Log("you got zapped!");
            FindObjectOfType<GameManager>().AddHealth(-damage);
        }
    }
}