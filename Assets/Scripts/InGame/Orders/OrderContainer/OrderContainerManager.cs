using System.Collections.Generic;
using UnityEngine;

public class OrderContainerManager : MonoBehaviour
{
    // === UI containers ===
    [SerializeField] private List<GameObject> orderContainers = new();

    // === Properties ===
    public List<GameObject> OrderContainers => orderContainers;

    public void ChangeContainerColor(OrderData currentOrder, Dictionary<string, GameObject> orderUIDict)
    {
        if (orderUIDict.TryGetValue(currentOrder.OrderID, out GameObject orderUI))
        {
            if (orderUI.transform.parent.TryGetComponent<OrderContainerUI>(out var containerUI)) containerUI.SetWarningScroll(true);
        }
    }

    public void ResetContainerColor(GameObject orderContainer)
    {
        if (orderContainer.TryGetComponent<OrderContainerUI>(out var containerUI)) containerUI.SetWarningScroll(false);
    }

    public void BurnContainer(GameObject orderContainer)
    {
        if (orderContainer.TryGetComponent<OrderContainerUI>(out var containerUI)) containerUI.PlayBurningScrollAnim(orderContainers, orderContainer);
    }
}