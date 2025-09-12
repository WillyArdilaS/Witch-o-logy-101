using System;
using UnityEngine;

public class OrderCreator : MonoBehaviour
{
    // === Order data ===
    [SerializeField] private RecipeBook recipeBook;
    [SerializeField] private Sprite[] orderImages;

    // === Order creation ===
    [SerializeField] private float lifeTimeDefault;
    private int totalOrders = 0;
    private RecipeData randomRecipe;
    private BottleData.BottleType randomBottleType;

    public OrderData CreateOrder()
    {
        totalOrders++;

        randomRecipe = recipeBook.GetRandomRecipe();
        randomBottleType = GetRandomBottleType();

        OrderData newOrder = new(GenerateOrderID(), FindOrderImg(), lifeTimeDefault, randomRecipe, randomBottleType);
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
        foreach (var image in orderImages)
        {
            if (image.name != $"{randomRecipe.RecipeID}_{randomBottleType}") continue;

            return image;
        }

        return null;
    }
}