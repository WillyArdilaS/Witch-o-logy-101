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

    // === State ===
    private bool isOrderCorrect = false;

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
            Transform bottleParent = deliveredBottle.transform.parent;

            // Search for matches between the bottle type and any of the orders
            List<OrderData> candidateOrders = orderManagerScript.ActiveOrders.FindAll(order => order.RequiredBottle == bottleData.BottleID);

            foreach (var order in candidateOrders)
            {
                // An order matches the delivery
                if (CheckIngredients(deliveredBottle, order))
                {
                    isOrderCorrect = true;
                    order.State = OrderData.OrderState.Delivered;
                    StartCoroutine(InvokeOrderChecked(bottleParent));
                    return;
                }
            }

            // No order matches the delivery
            isOrderCorrect = false;
            StartCoroutine(InvokeOrderChecked(bottleParent));
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
    private IEnumerator InvokeOrderChecked(Transform bottleParent)
    {
        BottleSpriteChanger bottleSpriteChanger = bottleParent.GetComponentInChildren<BottleSpriteChanger>();
        bottleSpriteChanger.PlayValidateBottleAnim(isOrderCorrect);

        yield return null; // Wait for the animator to change state

        float validateBottleAnimDuration = bottleSpriteChanger.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / Math.Abs(bottleSpriteChanger.Animator.speed);
        yield return new WaitForSeconds(validateBottleAnimDuration); // Wait until the animation finishes to handle the OrderChecked event

        bottleParent.GetComponent<Respawner>().SubscribeToOrderCheckedEvent(this);
        bottleParent.GetComponent<BottleFiller>().SubscribeToOrderCheckedEvent(this);

        OrderChecked?.Invoke();
    }
}