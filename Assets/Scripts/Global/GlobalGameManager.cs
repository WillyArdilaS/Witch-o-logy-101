using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    // === Singleton ===
    public static GlobalGameManager instance;

    // === Managers ===
    private SceneSwitchManager sceneSwitchManager;

    // === Properties ===
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
        if (sceneSwitchManager == null) sceneSwitchManager = GetComponentInChildren<SceneSwitchManager>();
    }
}