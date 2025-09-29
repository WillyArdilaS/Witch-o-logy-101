using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;
    private OrderContainerUI orderContainerUIScript;

    // === Singleton ===
    public static GameManager instance;

    // === States ===
    public enum GameState { Playing, InPause, ShowingRecipeBook }
    [SerializeField] private GameState state = GameState.Playing;

    // === Game timer ===
    [Header("Game duration")]
    [SerializeField, Min(0)] private int minutes;
    [SerializeField, Range(0, 59)] private int seconds;
    private int gameDuration;
    private int gameTimer = 0;

    // === Score ===
    public static int starsEarned = 0;

    // === Coroutines ===
    private Coroutine gameTimerRoutine;

    // === Properties ===
    public GameState State { get => state; set => state = value; }

    void Awake()
    {
        orderManagerScript = GetComponentInChildren<OrderManager>();
        orderContainerUIScript = GetComponentInChildren<OrderContainerUI>();

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
        gameDuration = seconds + (minutes * 60);

        if (gameTimerRoutine != null) StopCoroutine(StartGameTimer());
        gameTimerRoutine = StartCoroutine(StartGameTimer());
    }

    void Update()
    {
        if (state == GameState.Playing || state == GameState.ShowingRecipeBook)
        {
            orderManagerScript.CanCreateNewOrder = CheckOrderCreationAllowed();
        }

        if (orderContainerUIScript.OrderContainers.Count == 0)
        {
            LoadGameOver(0);
        }
    }

    private IEnumerator StartGameTimer()
    {
        while (gameTimer < gameDuration)
        {
            yield return new WaitForSeconds(1);
            gameTimer++;
        }

        LoadGameOver(orderContainerUIScript.OrderContainers.Count);
    }

    private bool CheckOrderCreationAllowed()
    {
        if (orderManagerScript.ActiveOrders.Count >= orderContainerUIScript.OrderContainers.Count) return false;

        return true;
    }

    public void LoadGameOver(int stars)
    {
        starsEarned = stars;
        GlobalGameManager.instance.SceneSwitchManager.LoadScene("GameOver");
    }
}