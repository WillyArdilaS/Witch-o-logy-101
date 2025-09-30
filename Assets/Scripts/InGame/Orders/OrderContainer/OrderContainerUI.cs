using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OrderContainerUI : MonoBehaviour
{
    // === Animation ===
    private Animator animator;

    // === Coroutines ===
    private Coroutine deleteContainerRoutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWarningScroll(bool state)
    {
        if (animator != null) animator.SetBool("b_isWarning", state);
    }

    public void PlayBurningScrollAnim(List<GameObject> orderContainers, GameObject orderContainer)
    {
        if (animator == null) return;

        animator.SetBool("b_isBurning", true);

        if (deleteContainerRoutine != null) StopCoroutine(deleteContainerRoutine);
        deleteContainerRoutine = StartCoroutine(DeleteContainer(orderContainers, orderContainer));
    }

    private IEnumerator DeleteContainer(List<GameObject> orderContainers, GameObject orderContainer)
    {
        yield return null; // Wait for the animator to change state

        float burntScrollAnimDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / Math.Abs(animator.speed);
        yield return new WaitForSeconds(burntScrollAnimDuration); // Wait until the animation finishes to delete the order container

        orderContainers.Remove(orderContainer);
    }
}