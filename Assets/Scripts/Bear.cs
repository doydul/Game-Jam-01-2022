using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bear : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
    public int damage;
    public float damageInterval;
    public Transform spriteTransform;

    HeroMovement hero;
    Timer attackTimer;
    Timer soundTimer;
    Rigidbody2D rigidbody;
    GameManager manager;

    bool fleeing;
    float spriteXscale;
    
    void Awake() {
        hero = FindObjectOfType<HeroMovement>();
        rigidbody = GetComponent<Rigidbody2D>();
        attackTimer = new Timer(damageInterval);
        manager = FindObjectOfType<GameManager>();
        spriteXscale = spriteTransform.localScale.x;
        soundTimer = new Timer(6);
        Sounds.instance.BearSound();
    }

    void Update() {
        if (hero == null) return;
        if (soundTimer.Check()) Sounds.instance.BearSound();
        Vector2 diff = hero.transform.position - transform.position;
        if (diff.magnitude >= 20) Destroy(gameObject);
        if (fleeing) {
            rigidbody.AddForce(diff.normalized * -acceleration);
        } else {
            rigidbody.AddForce(diff.normalized * acceleration);
        }
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
        var scale = spriteTransform.localScale;
        if (rigidbody.velocity.x > 0) {
            scale.x = -spriteXscale;
        } else {
            scale.x = spriteXscale;
        }
        spriteTransform.localScale = scale;
    }

    void OnCollisionStay2D(Collision2D other) {
        var otherHero = other.collider.GetComponent<HeroMovement>();
        if (otherHero != null && attackTimer.Check()) {
            manager.AddHealth(-damage);
        }
    }

    public void Stop() {
        fleeing = true;
    }
}