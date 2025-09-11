using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    // === Order data ===
    [SerializeField] private RecipeBook recipeBook;
    [SerializeField] private Sprite[] orderImages;

    [Header("Order List")]
    [SerializeField] private List<OrderData> activeOrders = new();

    // === Order creation ===
    [Header("Order creation info")]
    [SerializeField] private float lifeTimeDefault;
    private int totalOrders = 0;
    private RecipeData randomRecipe;
    private BottleData.BottleType randomBottleType;

    // === Events ===
    public event Action<OrderData> OrderAdded;

    void Awake()
    {
        AddOrder();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            AddOrder();
        }
    }

    private void AddOrder()
    {
        totalOrders++;

        randomRecipe = recipeBook.GetRandomRecipe();
        randomBottleType = GetRandomBottleType();

        OrderData newOrder = new(GenerateOrderID(), FindOrderImg(), lifeTimeDefault, randomRecipe, randomBottleType);
        activeOrders.Add(newOrder);
        OrderAdded?.Invoke(newOrder);
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
        foreach (var image in orderImages)
        {
            if (image.name != $"{randomRecipe.RecipeID}_{randomBottleType}") continue;

            return image;
        }

        return null;
    }
}