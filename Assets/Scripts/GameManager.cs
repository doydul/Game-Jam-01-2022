using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    
    public TMP_Text woodCounter;
    public TMP_Text stoneCounter;

    int woodCount;
    int stoneCount;

    public void AddWood(int amount) {
        woodCount += amount;
        woodCounter.text = woodCount.ToString();
    }

    public void AddStone(int amount) {
        stoneCount += amount;
        stoneCounter.text = stoneCount.ToString();
    }
}