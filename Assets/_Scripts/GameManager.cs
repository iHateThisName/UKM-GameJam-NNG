using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public static void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
