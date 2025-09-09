using System;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    // === Events ===
    public event Action<DraggableItem> IngredientDropped;
    public event Action<IngredientContainer> BottleDropped;

    public void OnItemDrop(DraggableItem item)
    {
        Debug.Log("Se ha agregado: " + item.ItemData.ItemName);

        if (item.ItemData.Type.ToString() == "Ingredient")
        {
            item.GetComponentInParent<Respawner>().SubscribeToIngredientDroppedEvent(this);
            IngredientDropped?.Invoke(item);
        }
        else if (item.ItemData.Type.ToString() == "Bottle")
        {
            item.GetComponentInParent<BottleFiller>().SubscribeToBottleDroppedEvent(this);
            item.GetComponentInParent<BottleDeliverer>().SubscribeToBottleDroppedEvent(this);
            BottleDropped?.Invoke(gameObject.GetComponent<IngredientContainer>());
        }
    }
}