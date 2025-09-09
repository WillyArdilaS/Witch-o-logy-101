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
    private Action<IngredientContainer> bottleDroppedAction;

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
        bottleDroppedAction = _ => DeliverOrder(); // To save a reference to the event due to lambda
        dropAreaScript.BottleDropped += bottleDroppedAction;
    }

    public void UnsubscribeToBottleDropped()
    {
        if (dropAreaScript != null && bottleDroppedAction != null)
        {
            dropAreaScript.BottleDropped -= bottleDroppedAction;
            bottleDroppedAction = null;
        }
    }

    // === Delivery methods ===
    private void DeliverOrder()
    {
        bottle.transform.position = tablePosition;
        UnsubscribeToBottleDropped();
    }
}