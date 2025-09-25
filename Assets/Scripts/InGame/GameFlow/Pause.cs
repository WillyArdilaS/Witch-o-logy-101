using UnityEngine;

public class Pause : MonoBehaviour
{
    // === UI ===
    [SerializeField] private GameObject pauseUI;

    // === Game state ===
    private GameManager.GameState previousState;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.State != GameManager.GameState.InPause)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        previousState = GameManager.instance.State;
        GameManager.instance.State = GameManager.GameState.InPause;

        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame() {
        GameManager.instance.State = previousState;
        
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}