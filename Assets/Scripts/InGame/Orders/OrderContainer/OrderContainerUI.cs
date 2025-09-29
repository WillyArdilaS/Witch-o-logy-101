using System.Collections.Generic;
using UnityEngine;

public class OrderContainerUI : MonoBehaviour
{
    // === UI containers ===
    [SerializeField] private List<GameObject> orderContainers = new();

    // === Properties ===
    public List<GameObject> OrderContainers => orderContainers;

    public void ChangeContainerColor(OrderData currentOrder, Dictionary<string, GameObject> orderUIDict)
    {
        if (orderUIDict.TryGetValue(currentOrder.OrderID, out GameObject orderUI))
        {
            if (orderUI.transform.parent.TryGetComponent<OrderContainerManager>(out var containerManager)) containerManager.SetWarningScroll(true);
        }
    }

    public void ResetContainerColor(GameObject orderContainer)
    {
        if (orderContainer.TryGetComponent<OrderContainerManager>(out var containerManager)) containerManager.SetWarningScroll(false);
    }

    public void BurnContainer(GameObject orderContainer)
    {
        if (orderContainer.TryGetComponent<OrderContainerManager>(out var containerManager)) containerManager.PlayBurningScrollAnim(orderContainers, orderContainer);
    }
}