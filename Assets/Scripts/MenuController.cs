using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    
    public Button button;

    void Awake() {
        button.onClick.AddListener(GoToGame);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            GoToGame();
        }
    }

    public void GoToGame() {
        SceneManager.LoadScene("Main");
    }
}