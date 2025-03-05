using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public int Score = 0;
    public void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
