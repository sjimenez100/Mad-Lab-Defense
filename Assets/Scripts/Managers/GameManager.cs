using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    private enum GameState {Play, Pause, End}
    private GameState currentState;
    private GameState previousState;


    private void Awake() => main = this;

    private void OnEnable()
    {
        Time.timeScale = 1;
        EventManager.main.gameStateToggleEvent += StateToggle;
    }

    private void StateToggle()
    {
        if (currentState == GameState.Pause)
            FallBack();
        else
            ChangeState(GameState.Pause);

        
    }
    private void FallBack()
    {
        ChangeState(previousState);
    }

    private void ChangeState(GameState toState)
    {
        switch (toState)
        {

            case GameState.Play:
                EventManager.main.OnGamePlay();
                Time.timeScale = 1;
                break;

            case GameState.Pause:
                EventManager.main.OnGamePause();
                Time.timeScale = 0;
                break;

            case GameState.End:
                EventManager.main.OnGameOver();
                break;

            default:
                break;
        }

        currentState = toState;

    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
