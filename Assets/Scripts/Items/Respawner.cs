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

    // === Event handler ===
    private Action<DraggableItem> ingredientDroppedAction;

    // === Coroutines ===
    private Coroutine cooldownRoutine;

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

        orderCheckerScript = GetComponent<OrderChecker>();
        if (orderCheckerScript != null)
        {
            orderCheckerScript.OrderChecked += StartCooldown;
        }
    }

    // === Event subscriptions methods ===
    public void SubscribeToIngredientDroppedEvent(DropArea dropArea)
    {
        dropAreaScript = dropArea;
        ingredientDroppedAction = _ => StartCooldown(); // To save a reference to the event due to lambda
        dropAreaScript.IngredientDropped += ingredientDroppedAction;
    }

    public void UnsubscribeToIngredientDropped()
    {
        if (dropAreaScript != null && ingredientDroppedAction != null)
        {
            dropAreaScript.IngredientDropped -= ingredientDroppedAction;
            ingredientDroppedAction = null;
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
        UnsubscribeToIngredientDropped();
    }
}