using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderManager))]
public class OrderChecker : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;

    // === Bottles ===
    [SerializeField] private BottleDeliverer[] bottleDeliverers;

    // === Events ===
    public event Action OrderChecked;

    void Awake()
    {
        orderManagerScript = GetComponent<OrderManager>();

        foreach (var deliverer in bottleDeliverers)
        {
            deliverer.BottleDelivered += CheckBottle;
        }
    }

    void OnDestroy()
    {
        foreach (var deliverer in bottleDeliverers)
        {
            deliverer.BottleDelivered -= CheckBottle;
        }
    }

    private void CheckBottle(GameObject deliveredBottle)
    {
        if (!deliveredBottle.TryGetComponent<DraggableItem>(out var draggable))
        {
            Debug.LogWarning("El objeto entregado no tiene DraggableItem");
            return;
        }

        // Downcast safety to convert ItemData to BottleData
        if (draggable.ItemData is BottleData bottleData)
        {
            var parentRespawner = deliveredBottle.GetComponentInParent<Respawner>();

            // Search for matches between the bottle type and any of the orders
            List<OrderData> candidateOrders = orderManagerScript.ActiveOrders.FindAll(order => order.RequiredBottle == bottleData.BottleID);

            foreach (var order in candidateOrders)
            {
                if (CheckIngredients(deliveredBottle, order))
                {
                    Debug.Log("La orden es correcta");
                    order.State = OrderData.OrderState.Delivered;

                    StartCoroutine(InvokeOrderChecked(1f, deliveredBottle));
                    return;
                }
            }

            Debug.Log("Ninguna orden coincide con esa botella + ingredientes");
            StartCoroutine(InvokeOrderChecked(1f, deliveredBottle));
        }
    }

    private bool CheckIngredients(GameObject deliveredBottle, OrderData order)
    {
        List<IngredientData> delivered = deliveredBottle.GetComponentInParent<BottleFiller>().IngredientListInBottle;
        IngredientData[] required = order.RequiredRecipe.RequiredIngredients;

        // Look for matches between the ingredients on the bottle and those requested
        foreach (var ingredient in required)
        {
            if (!delivered.Contains(ingredient)) return false;
        }

        return true;
    }

    // === Activate respawn with delay method ===
    private IEnumerator InvokeOrderChecked(float delay, GameObject deliveredBottle)
    {
        yield return new WaitForSeconds(delay);

        deliveredBottle.GetComponentInParent<Respawner>().SubscribeToOrderCheckedEvent(this);
        OrderChecked?.Invoke();
    }

    // public void RespawnAfterAnimation()
    // {
    //     deliveredBottle.GetComponentInParent<Respawner>().SubscribeToOrderCheckedEvent(this);
    //     OrderChecked?.Invoke();
    // }
}