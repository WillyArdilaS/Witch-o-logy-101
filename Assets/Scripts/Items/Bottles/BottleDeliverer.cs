using System;
using UnityEngine;

public class BottleDeliverer : MonoBehaviour
{
    // === Scripts ===
    private DropArea dropAreaScript;

    // === Bottle repositioning ===
    [SerializeField] private Vector2 tablePosition;
    private GameObject bottle;
    
    // === Event handler ===
    private Action<IngredientContainer> onBottleDroppedHandler;

    void Awake()
    {
        if (transform.childCount > 0)
        {
            bottle = transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogWarning("Este deliverer no tiene ninguna botella asociada");
        }
    }

    // === Event subscriptions methods ===
    public void SubscribeToBottleDroppedEvent(DropArea dropArea)
    {
        dropAreaScript = dropArea;
        onBottleDroppedHandler = _ => DeliverOrder(); // To save a reference to the event due to lambda
        dropAreaScript.BottleDropped += onBottleDroppedHandler;
    }

    public void UnsubscribeToBottleDropped()
    {
        if (dropAreaScript != null && onBottleDroppedHandler != null)
        {
            dropAreaScript.BottleDropped -= onBottleDroppedHandler;
            onBottleDroppedHandler = null;
        }
    }

    // === Delivery methods ===
    private void DeliverOrder()
    {
        bottle.transform.position = tablePosition;
        UnsubscribeToBottleDropped();
    }
}