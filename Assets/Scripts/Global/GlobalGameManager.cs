using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour
{
    // === Singleton ===
    public static GlobalGameManager instance;

    // === Managers ===
    private AudioManager audioManager;
    private SceneSwitchManager sceneSwitchManager;

    // === Properties ===
    public AudioManager AudioManager => audioManager;
    public SceneSwitchManager SceneSwitchManager => sceneSwitchManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        if (audioManager == null) audioManager = GetComponentInChildren<AudioManager>();
        if (sceneSwitchManager == null) sceneSwitchManager = GetComponentInChildren<SceneSwitchManager>();

        InitializeGameSettings();
    }

    private void InitializeGameSettings()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                audioManager.PlayMusic(AudioManager.MusicType.MainMenu, 0, audioManager.MainMenuSongVol);
                break;
            case "GameScene":
                audioManager.PlayMusic(AudioManager.MusicType.InGame, 0, audioManager.GameSongVol);
                break;
        }
    }
}