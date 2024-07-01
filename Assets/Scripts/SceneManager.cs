using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public void Gameplay() {
        SceneManager.LoadScene(2);
    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public static void Lost() {
        SceneManager.LoadScene(1);
    }

    public static void Win() {
        SceneManager.LoadScene(3);
    }
}