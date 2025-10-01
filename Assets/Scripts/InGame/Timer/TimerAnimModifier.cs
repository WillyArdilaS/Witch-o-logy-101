using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TimerAnimModifier : MonoBehaviour
{
    // === Timer animation ===
    private Animator timerAnim;
    private float animSpeed;
    private float timerClipDuration;

    // === Hourglass animation ===
    [SerializeField] private Animator hourglassAnim;

    void Start()
    {
        timerAnim = GetComponent<Animator>();
        hourglassAnim = transform.GetChild(0).GetComponent<Animator>();

        // Adjust the duration of the animation to the game time
        timerClipDuration = timerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        animSpeed = timerClipDuration / GameManager.instance.GameDuration; // Speed = clip length / game duration
        timerAnim.speed = animSpeed;
    }

    public void ChangeHourglassAnim()
    {
        hourglassAnim.SetBool("b_isTurningRed", true);
    }
}