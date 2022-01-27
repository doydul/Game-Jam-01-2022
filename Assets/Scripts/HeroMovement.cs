using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class HeroMovement : MonoBehaviour {
    
    public float acceleration;
    public float maxSpeed;
    public float stationaryThreshold;

    Rigidbody2D rigidbody;
    Animator animator;
    HeroResourceGathering resourceGathering;
    HeroBuildingPlacement buildingPlacement;
    
    bool inputDisabled;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        resourceGathering = FindObjectOfType<HeroResourceGathering>();
        buildingPlacement = FindObjectOfType<HeroBuildingPlacement>();
    }

    void Update() {
        if (inputDisabled) {
            resourceGathering.StopMining();
        } else {
            if (Input.GetKeyDown(KeyCode.K)) {
                if (buildingPlacement.active) {
                    buildingPlacement.Place();
                } else {
                    resourceGathering.Interact();
                }
            }
            if (Input.GetKeyDown(KeyCode.J)) {
                resourceGathering.StartMining();
            } else if (Input.GetKeyUp(KeyCode.J)) {
                resourceGathering.StopMining();
            }
            if (Input.GetKey(KeyCode.J)) {
                animator.SetBool("pickaxing", true);
            }else {
                animator.SetBool("pickaxing", false);
                if (Input.GetKey(KeyCode.W)) {
                    Move(0, 1);
                }
                if (Input.GetKey(KeyCode.S)) {
                    Move(0, -1);
                }
                if (Input.GetKey(KeyCode.A)) {
                    Move(-1, 0);
                }
                if (Input.GetKey(KeyCode.D)) {
                    Move(1, 0);
                }
            }
        }

        if (rigidbody.velocity.magnitude < stationaryThreshold) {
            animator.SetBool("stationary", true);
        } else {
            animator.SetBool("stationary", false);
            if (Mathf.Abs(rigidbody.velocity.x) > Mathf.Abs(rigidbody.velocity.y)) {
                if (rigidbody.velocity.x > 0) {
                    animator.SetFloat("direction", 1f);
                } else {
                    animator.SetFloat("direction", 0.666f);
                }
            } else {
                if (rigidbody.velocity.y > 0) {
                    animator.SetFloat("direction", 0f);
                } else {
                    animator.SetFloat("direction", 0.333f);
                }
            }
        }
    }

    void Move(float x, float y) {
        rigidbody.AddForce(new Vector2(x * acceleration, y * acceleration));
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }

    public void DisableInput() {
        inputDisabled = true;
    }

    public void EnableInput() {
        inputDisabled = false;
    }
}