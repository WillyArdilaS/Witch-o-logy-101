using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderCreator))]
public class OrderManager : MonoBehaviour
{
    // === Scripts ===
    private OrderCreator orderCreatorScript;

    // === Orders ===
    [SerializeField] private List<OrderData> activeOrders = new();
    private OrderData newOrder;

    // === Events ===
    public event Action<OrderData> OrderAdded;

    // === Properties ===
    public List<OrderData> ActiveOrders => activeOrders;

    void Awake()
    {
        orderCreatorScript = GetComponent<OrderCreator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            newOrder = orderCreatorScript.CreateOrder();
            activeOrders.Add(newOrder);
            OrderAdded?.Invoke(newOrder);
        }
    }
}