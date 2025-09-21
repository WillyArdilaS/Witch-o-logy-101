using System;
using System.Linq;
using UnityEngine;

public class OrderCreator : MonoBehaviour
{
    // === Order data ===
    [SerializeField] private RecipeBookData recipeBook;
    [SerializeField] private Sprite[] orderImages;

    // === Order creation ===
    [SerializeField] private float lifeTimeDefault;
    private int totalOrders = 0;
    private RecipeData randomRecipe;
    private BottleData.BottleType randomBottleType;

    // === Coroutines ===
    private Coroutine lifeTimerRoutine;

    public OrderData CreateOrder()
    {
        totalOrders++;

        randomRecipe = recipeBook.GetRandomRecipe();
        randomBottleType = GetRandomBottleType();

        OrderData newOrder = new(GenerateOrderID(), FindOrderImg(), lifeTimeDefault, randomRecipe, randomBottleType);
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

    private Sprite FindOrderImg()
    {
        return orderImages.FirstOrDefault(img => img.name == $"{randomRecipe.RecipeID}_{randomBottleType}");
    }

    public void StartOrderLifeTimer(OrderData order)
    {
        if (lifeTimerRoutine != null) StopCoroutine(order.LifeTimer());
        lifeTimerRoutine = StartCoroutine(order.LifeTimer());
    }
}