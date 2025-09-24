using System;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    // === Events ===
    public event Action<DraggableItem> IngredientDropped;
    public event Action<GameObject> BottleDropped;

    public void OnItemDrop(DraggableItem item)
    {
        if (item.ItemData.Type == ItemData.ItemType.Ingredient)
        {
            item.GetComponentInParent<Respawner>().SubscribeToIngredientDroppedEvent(this);
            IngredientDropped?.Invoke(item);
        }
        else if (item.ItemData.Type == ItemData.ItemType.Bottle)
        {
            item.GetComponentInParent<BottleFiller>().SubscribeToBottleDroppedEvent(this);
            item.GetComponentInParent<BottleDeliverer>().SubscribeToBottleDroppedEvent(this);
            BottleDropped?.Invoke(gameObject);
        }
    }
}