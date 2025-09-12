using UnityEngine;

public class GameManager : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;

    void Awake()
    {
        orderManagerScript = GetComponentInChildren<OrderManager>();
    }

    void Update()
    {
        orderManagerScript.CanCreateNewOrder = orderManagerScript.ActiveOrders.Count < 3;
    }
}
