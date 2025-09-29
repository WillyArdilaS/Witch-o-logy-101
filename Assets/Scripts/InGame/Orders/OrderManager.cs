using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderCreator), typeof(OrderContainerUI))]
public class OrderManager : MonoBehaviour
{
    // === Scripts ===
    private OrderCreator orderCreatorScript;
    private OrderUI orderUIScript;
    private OrderContainerUI orderContainerUIScript;

    // === Order creation ===
    [SerializeField] private float orderCooldown;
    [SerializeField] private float orderWarningTime;
    [SerializeField] private List<OrderData> activeOrders = new();
    private OrderData newOrder;
    private bool canCreateNewOrder = true;

    // === Coroutines ===
    private Coroutine createOrderRoutine;

    // === Events ===
    public event Action<OrderData> OrderAdded;
    public event Action<OrderData> OrderDelivered;
    public event Action<OrderData> OrderFailed;

    // === Properties ===
    public List<OrderData> ActiveOrders => activeOrders;
    public float OrderCooldown { get => orderCooldown; set => orderCooldown = value; }
    public bool CanCreateNewOrder { get => canCreateNewOrder; set => canCreateNewOrder = value; }

    void Awake()
    {
        orderCreatorScript = GetComponent<OrderCreator>();
        orderUIScript = GetComponent<OrderUI>();
        orderContainerUIScript = GetComponent<OrderContainerUI>();
    }

    void Update()
    {
        // Management of new order creation
        if (canCreateNewOrder && createOrderRoutine == null)
        {
            createOrderRoutine = StartCoroutine(CreateNewOrder());
        }
        else if (!canCreateNewOrder && createOrderRoutine != null)
        {
            StopCoroutine(createOrderRoutine);
            createOrderRoutine = null;
        }

        // Review and deletion of delivered or failed orders
        List<OrderData> ordersUpdated = activeOrders.FindAll(order => (order.State == OrderData.OrderState.Delivered) || (order.State == OrderData.OrderState.Failed));
        foreach (var order in ordersUpdated)
        {
            DeleteOrder(order);
        }

        // Validate the order duration to change the color of the container
        foreach (var order in activeOrders)
        {
            if (order.LifeTime <= orderWarningTime)
            {
                orderContainerUIScript.ChangeContainerColor(order, orderUIScript.OrderUIDict);
            }
        }
    }

    private IEnumerator CreateNewOrder()
    {
        while (activeOrders.Count < orderContainerUIScript.OrderContainers.Count)
        {
            yield return new WaitForSeconds(orderCooldown);

            newOrder = orderCreatorScript.CreateOrder();
            activeOrders.Add(newOrder);
            OrderAdded?.Invoke(newOrder);
        }
    }

    private void DeleteOrder(OrderData orderToDelete)
    {
        if (orderToDelete.State == OrderData.OrderState.Delivered)
        {
            activeOrders.Remove(orderToDelete);
            OrderDelivered?.Invoke(orderToDelete);
        }
        else if (orderToDelete.State == OrderData.OrderState.Failed)
        {
            activeOrders.Remove(orderToDelete);
            OrderFailed?.Invoke(orderToDelete);
        }
    }
}