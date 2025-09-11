using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderGenerator))]
public class OrderUI : MonoBehaviour
{
    // === Scripts ===
    private OrderGenerator orderGeneratorScript;

    // === UI containers ===
    [SerializeField] private GameObject[] orderSpaces;

    // === Instantiation ===
    [SerializeField] private GameObject orderPrefab;
    private GameObject orderUI;

    void Awake()
    {
        orderGeneratorScript = GetComponent<OrderGenerator>();

        orderGeneratorScript.OrderAdded += ShowOrder;
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