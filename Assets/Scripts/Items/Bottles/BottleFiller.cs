using System.Collections.Generic;
using UnityEngine;

public class BottleFiller : MonoBehaviour
{
    // === Scripts ===
    private DropArea dropAreaScript;
    private OrderChecker orderCheckerScript;

    // === Ingredients ===
    [SerializeField] private List<IngredientData> ingredientListInBottle = new();

    // === Properties ===
    public List<IngredientData> IngredientListInBottle => ingredientListInBottle;

    // === Event subscriptions methods ===
    public void SubscribeToBottleDroppedEvent(DropArea dropArea)
    {
        dropAreaScript = dropArea;
        dropAreaScript.BottleDropped += FillBottle;
    }

    public void SubscribeToOrderCheckedEvent(OrderChecker orderChecker)
    {
        orderCheckerScript = orderChecker;
        orderChecker.OrderChecked += EmptyBottle;
    }

    public void UnsubscribeToBottleDroppedEvent()
    {
        dropAreaScript.BottleDropped -= FillBottle;
    }

    public void UnsubscribeToOrderCheckedEvent()
    {
        if (orderCheckerScript != null)
        {
            orderCheckerScript.OrderChecked -= EmptyBottle;
        }
    }

    // === List management methods ===
    private void FillBottle(IngredientContainer container)
    {
        foreach (var ingredient in container.IngredientList)
        {
            ingredientListInBottle.Add(ingredient);
        }

        container.EmptyContainer();
    }

    private void EmptyBottle()
    {
        ingredientListInBottle.Clear();
        UnsubscribeToBottleDroppedEvent();
    }
}