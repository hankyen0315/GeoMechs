using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Normal = 0, Pause = 1
    }
    public GameState State { get; private set; } = GameState.Normal;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        State = GameState.Pause;
        // Additional logic for pausing the game, e.g., showing a pause menu
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        State = GameState.Normal;
        // Additional logic for resuming the game, e.g., hiding a pause menu
    }

}
