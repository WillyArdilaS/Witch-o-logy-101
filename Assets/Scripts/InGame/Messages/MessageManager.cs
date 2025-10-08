using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MessageManager : MonoBehaviour
{
    // === Fade animation ===
    [SerializeField] private float fadeOutDelay;
    private Animator animator;

    // === Properties ===
    public Animator Animator => animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float fadeAnimDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        yield return new WaitForSeconds(fadeAnimDuration + fadeOutDelay); // Wait for the duration of the fade in animation plus the delay before the fade out
        animator.SetBool("b_isFadingOut", true);
    }

    private void FadeOutComplete()
    {
        Destroy(gameObject);
    }
}