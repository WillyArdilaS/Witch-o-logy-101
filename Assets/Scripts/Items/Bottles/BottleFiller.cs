using System.Collections.Generic;
using UnityEngine;

public class BottleFiller : MonoBehaviour
{
    // === Scripts ===
    private DropArea dropAreaScript;

    // === Ingredients ===
    [SerializeField] private List<DraggableItem> ingredientListInBottle;

    // === Event subscriptions methods ===
    public void SubscribeToBottleDroppedEvent(DropArea dropArea)
    {
        dropAreaScript = dropArea;
        dropAreaScript.BottleDropped += FillBottle;
    }

    public void UnsubscribeToBottleDroppedEvent()
    {
        dropAreaScript.BottleDropped -= FillBottle;
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
    }
}