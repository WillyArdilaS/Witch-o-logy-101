using UnityEngine;

public class GameManager : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;
    private OrderUI orderUIScript;

    void Awake()
    {
        orderManagerScript = GetComponentInChildren<OrderManager>();
        orderUIScript = GetComponentInChildren<OrderUI>();
    }

    void Update()
    {
        orderManagerScript.CanCreateNewOrder = orderManagerScript.ActiveOrders.Count < orderUIScript.OrderContainers.Count;
    }
}