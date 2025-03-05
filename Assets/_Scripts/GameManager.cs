using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public static int Score = 0;
    public static void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
