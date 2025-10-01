using System.Collections.Generic;
using UnityEngine;

public class BottleFiller : MonoBehaviour
{
    // === Scripts ===
    private DropArea dropAreaScript;
    private BottleSpriteChanger bottleColorChangerScript;
    private OrderChecker orderCheckerScript;

    // === Ingredients ===
    [SerializeField] private List<IngredientData> ingredientListInBottle = new();

    // === Properties ===
    public List<IngredientData> IngredientListInBottle => ingredientListInBottle;

    void Awake()
    {
        bottleColorChangerScript = GetComponentInChildren<BottleSpriteChanger>();
    }

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
    private void FillBottle(GameObject cauldron)
    {
        IngredientContainer ingredientContainer = cauldron.GetComponent<IngredientContainer>();

        foreach (var ingredient in ingredientContainer.IngredientList)
        {
            ingredientListInBottle.Add(ingredient);
        }

        bottleColorChangerScript.ChangeBottleSprite(cauldron);
        ingredientContainer.EmptyContainer();
    }

    private void EmptyBottle()
    {
        ingredientListInBottle.Clear();
        bottleColorChangerScript.ResetBottleSprite();
        UnsubscribeToBottleDroppedEvent();
    }
}