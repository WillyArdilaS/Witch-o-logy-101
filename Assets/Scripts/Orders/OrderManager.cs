using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(OrderCreator))]
public class OrderManager : MonoBehaviour
{
    // === Scripts ===
    private OrderCreator orderCreatorScript;

    // === Order creation ===
    [SerializeField] private float orderCooldown;
    [SerializeField] private List<OrderData> activeOrders = new();
    private OrderData newOrder;
    [SerializeField] private bool canCreateNewOrder = true;

    // === Coroutines ===
    private Coroutine createOrderRoutine;

    // === Events ===
    public event Action<OrderData> OrderAdded;

    // === Properties ===
    public List<OrderData> ActiveOrders => activeOrders;
    public float OrderCooldown { get => orderCooldown; set => orderCooldown = value; }
    public bool CanCreateNewOrder { get => canCreateNewOrder; set => canCreateNewOrder = value; }

    void Awake()
    {
        orderCreatorScript = GetComponent<OrderCreator>();
    }

    void Update()
    {
        if (canCreateNewOrder && createOrderRoutine == null)
        {
            createOrderRoutine = StartCoroutine(CreateNewOrder());
        }
        else if (!canCreateNewOrder && createOrderRoutine != null)
        {
            StopCoroutine(createOrderRoutine);
            createOrderRoutine = null;
        }
    }

    private IEnumerator CreateNewOrder()
    {
        while (activeOrders.Count < 3)
        {
            yield return new WaitForSeconds(orderCooldown);

            newOrder = orderCreatorScript.CreateOrder();
            activeOrders.Add(newOrder);
            OrderAdded?.Invoke(newOrder);
        }
    }
}