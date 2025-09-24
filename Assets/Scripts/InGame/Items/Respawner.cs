using System;
using System.Collections;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    // === Scripts ===
    private DraggableItem draggableItemScript;
    private DeadZoneDetector detectDeadZoneScript;
    private DropArea dropAreaScript;
    private OrderChecker orderCheckerScript;

    // === Item ===
    private GameObject itemToRespawn;

    // === Coroutines ===
    private Coroutine cooldownRoutine;

    // === Event handlers ===
    private Action<DraggableItem> onIngredientDroppedHandler;

    void Awake()
    {
        draggableItemScript = GetComponentInChildren<DraggableItem>();
        detectDeadZoneScript = GetComponentInChildren<DeadZoneDetector>();

        detectDeadZoneScript.DeadZoneEntered += StartCooldown;

        if (transform.childCount > 0)
        {
            itemToRespawn = transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogWarning("Este respawner no tiene ningun item asociado");
        }
    }

    // === Event subscriptions methods ===
    public void SubscribeToIngredientDroppedEvent(DropArea dropArea)
    {
        dropAreaScript = dropArea;
        onIngredientDroppedHandler = _ => StartCooldown(); // To save a reference to the event due to lambda
        dropAreaScript.IngredientDropped += onIngredientDroppedHandler;
    }

    public void SubscribeToOrderCheckedEvent(OrderChecker orderChecker)
    {
        orderCheckerScript = orderChecker;
        orderChecker.OrderChecked += StartCooldown;
    }

    public void UnsubscribeToIngredientDroppedEvent()
    {
        if (dropAreaScript != null && onIngredientDroppedHandler != null)
        {
            dropAreaScript.IngredientDropped -= onIngredientDroppedHandler;
            onIngredientDroppedHandler = null;
        }
    }

    public void UnsubscribeToOrderCheckedEvent()
    {
        if (orderCheckerScript != null)
        {
            orderCheckerScript.OrderChecked -= StartCooldown;
        }
    }

    // === Respawning methods ===
    private void StartCooldown()
    {
        itemToRespawn.SetActive(false);
        if (cooldownRoutine != null) StopCoroutine(cooldownRoutine);
        cooldownRoutine = StartCoroutine(CooldownForRespawn());
    }

    private IEnumerator CooldownForRespawn()
    {
        yield return new WaitForSeconds(draggableItemScript.ItemData.RespawnTime);
        Respawn();
    }

    private void Respawn()
    {
        itemToRespawn.transform.position = draggableItemScript.ItemData.StartPosition;
        draggableItemScript.ResetItem();
        
        UnsubscribeToIngredientDroppedEvent();
        UnsubscribeToOrderCheckedEvent();
    }
}