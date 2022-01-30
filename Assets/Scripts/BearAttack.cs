using UnityEngine;

public class BearAttack : MonoBehaviour {

    public float attackDuration;
    public GameObject bearPrefab;
    public Transform[] spawnPoints;

    Timer timer;
    Bear bear;

    void Awake() {
        timer = new Timer(attackDuration);
    }

    void Update() {
        if (bear != null && timer.Check()) {
            StopAttack();
        }
    }

    void SpawnBear() {
        var spawnPoint = spawnPoints[Mathf.FloorToInt(Random.value * spawnPoints.Length)];
        var bearGO = Instantiate(bearPrefab);
        bearGO.transform.position = spawnPoint.position;
        bear = bearGO.GetComponent<Bear>();
    }
    
    public void StartBearAttack() {
        timer.Start();
        SpawnBear();
    }

    public void StopAttack() {
        bear.Stop();
        bear = null;
    }
}