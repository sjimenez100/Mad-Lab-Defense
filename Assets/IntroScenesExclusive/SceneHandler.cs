using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler main;

    public void Awake()
    {
        Time.timeScale= 1.0f;
    }

    public void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void PreviousScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void MainMenu() => SceneManager.LoadScene("TitleScreen");

    public void MainScene() => SceneManager.LoadScene("MainScene");

    public void Quit() => Application.Quit();


}
