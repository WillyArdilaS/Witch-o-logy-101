using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderContainerUI))]
public class OrderUI : MonoBehaviour
{
    // === Scripts ===
    private OrderManager orderManagerScript;
    private OrderContainerUI orderContainerUIScript;

    // === Instantiation ===
    [SerializeField] private GameObject orderPrefab;
    private Dictionary<string, GameObject> orderUIDict = new();
    private GameObject orderUI;

    // === Coroutines ===
    private Coroutine deleteOrderRoutine;

    // === Properties ===
    public Dictionary<string, GameObject> OrderUIDict => orderUIDict;

    void Awake()
    {
        orderManagerScript = GetComponent<OrderManager>();
        orderContainerUIScript = GetComponent<OrderContainerUI>();

        if (orderManagerScript != null)
        {
            orderManagerScript.OrderAdded += ShowOrderUI;
            orderManagerScript.OrderDelivered += PlayFadeOutOrderAnim;
            orderManagerScript.OrderFailed += PlayFadeOutOrderAnim;
        }
    }

    private void ShowOrderUI(OrderData lastOrder)
    {
        foreach (var container in orderContainerUIScript.OrderContainers)
        {
            if (container.transform.childCount != 0) continue;

            orderUI = Instantiate(orderPrefab, container.transform);
            orderUI.GetComponent<Animator>().runtimeAnimatorController = lastOrder.AnimController;
            orderUIDict[lastOrder.OrderID] = orderUI;
            return;
        }
    }

    private void PlayFadeOutOrderAnim(OrderData orderToHide)
    {
        if (orderUIDict.TryGetValue(orderToHide.OrderID, out GameObject orderUI))
        {
            Animator orderUIAnimator = orderUI.GetComponent<Animator>();
            float fadeOutAnimDuration;

            orderUIAnimator.SetBool("b_isFadingOut", true);
            fadeOutAnimDuration = orderUIAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / Math.Abs(orderUIAnimator.speed);

            // Wait until the animation finishes to delete the order
            if (deleteOrderRoutine != null) StopCoroutine(deleteOrderRoutine);
            deleteOrderRoutine = StartCoroutine(DeleteOrderUI(fadeOutAnimDuration, orderToHide));
        }
    }

    private IEnumerator DeleteOrderUI(float delay, OrderData orderToDelete)
    {
        yield return new WaitForSeconds(delay);

        if (orderUIDict.TryGetValue(orderToDelete.OrderID, out GameObject orderUI))
        {
            GameObject orderContainer = orderUI.transform.parent.gameObject;

            Destroy(orderUI);
            orderUIDict.Remove(orderToDelete.OrderID);

            // Change the container image based on the order status
            if (orderToDelete.State == OrderData.OrderState.Delivered)
            {
                orderContainerUIScript.ResetContainerColor(orderContainer);
            }
            else if (orderToDelete.State == OrderData.OrderState.Failed)
            {
                orderContainerUIScript.PlayBurningScrollAnim(orderContainer);
            }
        }
    }
}