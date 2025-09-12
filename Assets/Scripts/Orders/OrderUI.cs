using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderManager))]
public class OrderUI : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;

    // === UI containers ===
    [SerializeField] private GameObject[] orderSpaces;

    // === Instantiation ===
    [SerializeField] private GameObject orderPrefab;
    private GameObject orderUI;

    void Awake()
    {
        orderManagerScript = GetComponent<OrderManager>();

        orderManagerScript.OrderAdded += ShowOrder;
    }

    private void ShowOrder(OrderData lastOrder)
    {
        foreach (var space in orderSpaces)
        {
            if (space.transform.childCount != 0) continue;

            orderUI = Instantiate(orderPrefab, space.transform);
            orderUI.GetComponent<Image>().sprite = lastOrder.OrderImg;
            return;
        }
    }
}