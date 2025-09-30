using System;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class OrderCreator : MonoBehaviour
{
    // === Order data ===
    [SerializeField] private RecipeBookData recipeBook;
    [SerializeField] private AnimatorOverrideController[] orderAnimOverrides;

    // === Order creation ===
    [SerializeField] private float lifeTimeDefault;
    private int totalOrders = 0;
    private RecipeData randomRecipe;
    private BottleData.BottleType randomBottleType;

    // === Coroutines ===
    private Coroutine lifeTimerRoutine;

    // === Properties ===
    public RecipeBookData RecipeBook { get => recipeBook; set => recipeBook = value; }
    public float LifeTimeDefault { get => lifeTimeDefault; set => lifeTimeDefault = value; }

    public OrderData CreateOrder()
    {
        totalOrders++;

        randomRecipe = recipeBook.GetRandomRecipe();
        randomBottleType = GetRandomBottleType();

        OrderData newOrder = new(GenerateOrderID(), FindOrderAnimController(), lifeTimeDefault, randomRecipe, randomBottleType);
        StartOrderLifeTimer(newOrder);
        return newOrder;
    }

    private BottleData.BottleType GetRandomBottleType()
    {
        Array values = Enum.GetValues(typeof(BottleData.BottleType));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return (BottleData.BottleType)values.GetValue(randomIndex);
    }

    private string GenerateOrderID()
    {
        return $"ORD{totalOrders}_{randomRecipe.RecipeID}_{randomBottleType}";
    }

    private AnimatorOverrideController FindOrderAnimController()
    {
        return orderAnimOverrides.FirstOrDefault(controller => controller.name == $"{randomRecipe.RecipeID}_{randomBottleType}");
    }

    public void StartOrderLifeTimer(OrderData order)
    {
        if (lifeTimerRoutine != null) StopCoroutine(order.StartLifeTimer());
        lifeTimerRoutine = StartCoroutine(order.StartLifeTimer());
    }
}