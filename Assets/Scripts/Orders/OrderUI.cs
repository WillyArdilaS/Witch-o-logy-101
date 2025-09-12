using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderManager))]
public class OrderUI : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;

    // === UI containers ===
    [SerializeField] private List<GameObject> orderSpaces;

    // === Instantiation ===
    [SerializeField] private GameObject orderPrefab;
    private Dictionary<string, GameObject> orderUIDict = new();
    private GameObject orderUI;

    void Awake()
    {
        orderManagerScript = GetComponent<OrderManager>();

        orderManagerScript.OrderAdded += ShowOrderUI;
        orderManagerScript.OrderCompleted += DeleteOrderUI;
        orderManagerScript.OrderFailed += DeleteOrderUI;
    }

    private void ShowOrderUI(OrderData lastOrder)
    {
        foreach (var space in orderSpaces)
        {
            if (space.transform.childCount != 0) continue;

            orderUI = Instantiate(orderPrefab, space.transform);
            orderUI.GetComponent<Image>().sprite = lastOrder.OrderImg;
            orderUIDict[lastOrder.OrderID] = orderUI;
            return;
        }
    }

    private void DeleteOrderUI(OrderData orderToDelete)
    {
        if (orderUIDict.TryGetValue(orderToDelete.OrderID, out GameObject orderUI))
        {
            Destroy(orderUI);
            orderUIDict.Remove(orderToDelete.OrderID);
        }
    }
}