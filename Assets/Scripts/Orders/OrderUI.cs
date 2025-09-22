using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderManager))]
public class OrderUI : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;

    // === UI containers ===
    [SerializeField] private List<GameObject> orderContainers;
    [SerializeField] private Sprite[] containerImages;

    // === Instantiation ===
    [SerializeField] private GameObject orderPrefab;
    private Dictionary<string, GameObject> orderUIDict = new();
    private GameObject orderUI;

    // === Properties ===
    public List<GameObject> OrderContainers => orderContainers;

    void Awake()
    {
        orderManagerScript = GetComponent<OrderManager>();

        orderManagerScript.OrderAdded += ShowOrderUI;
        orderManagerScript.OrderDelivered += DeleteOrderUI;
        orderManagerScript.OrderFailed += DeleteOrderUI;
    }

    // === Order UI methods ===
    private void ShowOrderUI(OrderData lastOrder)
    {
        foreach (var container in orderContainers)
        {
            if (container.transform.childCount != 0) continue;

            orderUI = Instantiate(orderPrefab, container.transform);
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

            // Change the container image based on the order status
            GameObject orderContainer = orderUI.transform.parent.gameObject;

            if (orderToDelete.State == OrderData.OrderState.Delivered)
            {
                ResetContainerColor(orderContainer);
            }
            else if (orderToDelete.State == OrderData.OrderState.Failed)
            {
                DeleteContainer(orderContainer);
            }
        }
    }

    // === Order container methods ===
    public void ChangeContainerColor(OrderData currentOrder)
    {
        if (orderUIDict.TryGetValue(currentOrder.OrderID, out GameObject orderUI))
        {
            GameObject orderContainer = orderUI.transform.parent.gameObject;
            orderContainer.GetComponent<Image>().sprite = containerImages.FirstOrDefault(img => img.name == "WarningScroll");
        }
    }

    private void ResetContainerColor(GameObject orderContainer)
    {
        orderContainer.GetComponent<Image>().sprite = containerImages.FirstOrDefault(img => img.name == "NormalScroll");
    }

    private void DeleteContainer(GameObject orderContainer)
    {
        orderContainer.GetComponent<Image>().sprite = containerImages.FirstOrDefault(img => img.name == "BurntScroll");

        orderContainers.Remove(orderContainer);
    }
}