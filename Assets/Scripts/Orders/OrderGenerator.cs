using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    // === Orders ===
    [SerializeField] private RecipeBook recipeBook;
    [SerializeField] private List<OrderData> activeOrders = new();
    [SerializeField] private Sprite[] orderImages;
    
    // === Order creation ===
    private int totalOrders = 0;
    private RecipeData randomRecipe;
    private BottleData.BottleType randomBottleType;

    // === Events ===
    public event Action OrderAdded;

    void Awake()
    {
        AddOrder();
    }

    private void AddOrder()
    {
        totalOrders++;

        randomRecipe = recipeBook.GetRandomRecipe();
        randomBottleType = GetRandomBottleType();

        OrderData newOrder = new(GenerateOrderID(), FindOrderImg(), randomRecipe, randomBottleType);
        activeOrders.Add(newOrder);
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