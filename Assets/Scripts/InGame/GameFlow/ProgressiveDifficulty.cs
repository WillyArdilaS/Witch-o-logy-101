using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// === Structures ===
[Serializable]
public struct DifficultyEvent
{
    [Header("Time Settings")]
    [SerializeField, Min(0)] private int minutes;
    [SerializeField, Range(0, 59)] private int seconds;

    [Header("Order Settings")]
    [SerializeField] private bool canCreateNewOrder;
    [SerializeField] private RecipeBookData newRecipeBook;
    [SerializeField] private float newOrderLifeTime;
    [SerializeField] private float newOrderCooldown;
    [SerializeField] private float newOrderWarningTime;

    // === Properties ===
    public int Minutes => minutes;
    public int Seconds => seconds;
    public bool CanCreateNewOrder => canCreateNewOrder;
    public RecipeBookData NewRecipeBook => newRecipeBook;
    public float NewOrderLifeTime => newOrderLifeTime;
    public float NewOrderCooldown => newOrderCooldown;
    public float NewOrderWarningTime => newOrderWarningTime;
}


public class ProgressiveDifficulty : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;
    private OrderCreator orderCreatorScript;

    // === Difficulty events management ===
    [SerializeField] private DifficultyEvent[] difficultyEventsList;
    private DifficultyEvent currentDifficultyEvent;
    private Queue<int> pendingTimes = new();
    private int currentTime = 0;

    // === Coroutines ===
    private Coroutine changeDifficultyRoutine;

    // === Properties ===
    public DifficultyEvent CurrentDifficultyEvent => currentDifficultyEvent;

    void Start()
    {
        orderManagerScript = GetComponentInChildren<OrderManager>();
        orderCreatorScript = GetComponentInChildren<OrderCreator>();

        // Convert all timestamps into total seconds and sort them in ascending order
        IOrderedEnumerable<int> times = difficultyEventsList.Select(difEvent => (difEvent.Minutes * 60) + difEvent.Seconds).OrderBy(difEvent => difEvent);
        foreach (var time in times)
        {
            pendingTimes.Enqueue(time); // Add timestamp to queue for sequential processing
        }
    }

    void Update()
    {
        currentTime = GameManager.instance.CurrentGameTimer;

        // Check if there are pending timestamps AND if the current time matches the next scheduled timestamp
        if (pendingTimes.Count > 0 && currentTime == pendingTimes.Peek())
        {
            int matchTime = pendingTimes.Dequeue(); // Remove the matched timestamp from the queue
            currentDifficultyEvent = difficultyEventsList.First(difEvent => (difEvent.Minutes * 60) + difEvent.Seconds == matchTime); // Find the corresponding DifficultyEvent struct 

            if (changeDifficultyRoutine != null) StopCoroutine(changeDifficultyRoutine);
            StartCoroutine(ChangeDifficulty(currentDifficultyEvent));
        }
    }

    private IEnumerator ChangeDifficulty(DifficultyEvent difficultyEvent)
    {
        if (difficultyEvent.CanCreateNewOrder)
        {
            orderCreatorScript.RecipeBook = difficultyEvent.NewRecipeBook;
            orderCreatorScript.LifeTimeDefault = difficultyEvent.NewOrderLifeTime;
            orderManagerScript.OrderCooldown = difficultyEvent.NewOrderCooldown;
            orderManagerScript.OrderWarningTime = difficultyEvent.NewOrderWarningTime;
        }
        
        yield return null;
    }
}