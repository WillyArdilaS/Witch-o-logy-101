using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderContainerUI : MonoBehaviour
{
    // === UI containers ===
    [SerializeField] private List<GameObject> orderContainers = new();

    // === Coroutines ===
    private Coroutine deleteContainerRoutine;

    // === Properties ===
    public List<GameObject> OrderContainers => orderContainers;

    public void ChangeContainerColor(OrderData currentOrder, Dictionary<string, GameObject> orderUIDict)
    {
        if (orderUIDict.TryGetValue(currentOrder.OrderID, out GameObject orderUI))
        {
            GameObject orderContainer = orderUI.transform.parent.gameObject;
            orderContainer.GetComponent<Animator>().SetBool("b_isWarning", true);
        }
    }

    public void ResetContainerColor(GameObject orderContainer)
    {
        orderContainer.GetComponent<Animator>().SetBool("b_isWarning", false);
    }

    public void PlayBurningScrollAnim(GameObject orderContainer)
    {
        Animator orderContainerAnimator = orderContainer.GetComponent<Animator>();
        orderContainerAnimator.SetBool("b_isBurning", true);

        if (deleteContainerRoutine != null) StopCoroutine(deleteContainerRoutine);
        deleteContainerRoutine = StartCoroutine(DeleteContainer(orderContainerAnimator, orderContainer));
    }

    private IEnumerator DeleteContainer(Animator animator, GameObject orderContainer)
    {
        yield return null; // Wait for the animator to change state

        float burntScrollAnimDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / Math.Abs(animator.speed);
        yield return new WaitForSeconds(burntScrollAnimDuration); // Wait until the animation finishes to delete the order container

        orderContainers.Remove(orderContainer);
    }
}