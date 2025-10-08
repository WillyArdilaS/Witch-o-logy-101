using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;
    private OrderContainerManager orderContainerManagerScript;
    private ProgressiveDifficulty progressiveDifficultyScript;

    // === Singleton ===
    public static GameManager instance;

    // === States ===
    public enum GameState { Playing, InPause, ShowingRecipeBook }
    [SerializeField] private GameState state = GameState.Playing;

    // === Game timer ===
    [Header("Game duration")]
    [SerializeField, Min(0)] private int minutes;
    [SerializeField, Range(0, 59)] private int seconds;
    [SerializeField] private int currentGameTime = 0;
    private int gameDuration;

    // === Score ===
    public static int starsEarned = 0;

    // === Coroutines ===
    private Coroutine gameTimerRoutine;

    // === Properties ===
    public GameState State { get => state; set => state = value; }
    public int GameDuration => gameDuration;
    public int CurrentGameTimer => currentGameTime;

    void Awake()
    {
        Time.timeScale = 1;

        orderManagerScript = GetComponentInChildren<OrderManager>();
        orderContainerManagerScript = GetComponentInChildren<OrderContainerManager>();
        progressiveDifficultyScript = GetComponent<ProgressiveDifficulty>();
        
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize timer
        gameDuration = (minutes * 60) + seconds;

        if (gameTimerRoutine != null) StopCoroutine(StartGameTimer());
        gameTimerRoutine = StartCoroutine(StartGameTimer());
    }

    void Update()
    {
        if (state == GameState.Playing || state == GameState.ShowingRecipeBook)
        {
            orderManagerScript.CanCreateNewOrder = CheckOrderCreationAllowed();
        }

        if (orderContainerManagerScript.OrderContainers.Count == 0)
        {
            LoadGameOver(0);
        }
    }

    private IEnumerator StartGameTimer()
    {
        while (currentGameTime < gameDuration)
        {
            yield return new WaitForSeconds(1);
            currentGameTime++;
        }

        LoadGameOver(orderContainerManagerScript.OrderContainers.Count);
    }

    private bool CheckOrderCreationAllowed()
    {
        if (orderManagerScript.ActiveOrders.Count >= orderContainerManagerScript.OrderContainers.Count) return false;
        if (!progressiveDifficultyScript.CurrentDifficultyEvent.CanCreateNewOrder) return false;

        return true;
    }

    public void LoadGameOver(int stars)
    {
        starsEarned = stars;
        GlobalGameManager.instance.SceneSwitchManager.LoadScene("GameOver");
    }
}