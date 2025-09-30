using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(DraggableItem), typeof(SpriteRenderer), typeof(Animator))]
public class BottleSpriteChanger : MonoBehaviour
{
    // === Scripts ===
    private DraggableItem draggableItemScript;

    // === Sprite ===
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite emptyBottleImg;

    // === Animations ===
    [SerializeField] private AnimatorOverrideController[] bottleAnimOverrides;
    private Animator animator;
    private Animator bottleVFXAnimator;
    private RuntimeAnimatorController baseAnimController;
    private AnimatorOverrideController currentAnimController;

    // === Properties ===
    public Animator Animator => animator;

    void Awake()
    {
        draggableItemScript = GetComponent<DraggableItem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bottleVFXAnimator = transform.GetChild(0).GetComponent<Animator>();

        baseAnimController = animator.runtimeAnimatorController;
    }

    public void ChangeBottleSprite(GameObject cauldron)
    {
        RecipeData recipeInCauldron = cauldron.GetComponent<CauldronColorChanger>().MatchingRecipe;

        if (recipeInCauldron != null)
        {
            // Find animator controller depending on the match in the cauldron
            currentAnimController = FindBottleAnimController(recipeInCauldron.RecipeID.ToString());
        }
        else
        {
            currentAnimController = FindBottleAnimController("Wrong");
        }

        animator.runtimeAnimatorController = currentAnimController;

        // Nullify the matching recipe in the cauldron to restore its color
        cauldron.GetComponent<CauldronColorChanger>().MatchingRecipe = null;
    }

    public void PlayValidateBottleAnim(bool isOrderCorrect)
    {
        if (!currentAnimController.name.Contains("Wrong"))
        {
            if (isOrderCorrect)
            {
                GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Order, 0, GlobalGameManager.instance.AudioManager.CorrectOrderVol);
                animator.SetTrigger("t_isCorrect");
            }
            else
            {
                GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Order, 3, GlobalGameManager.instance.AudioManager.WrongOrderVol);
                bottleVFXAnimator.SetTrigger("t_isTransforming");
                animator.SetTrigger("t_isWrong");
            }
        }
        else
        {
            GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Order, 3, GlobalGameManager.instance.AudioManager.WrongOrderVol);
            animator.SetTrigger("t_isWrong");
        }
    }

    public void ResetBottleSprite()
    {
        currentAnimController = null;
        animator.runtimeAnimatorController = baseAnimController;
        spriteRenderer.sprite = emptyBottleImg;
    }

    private AnimatorOverrideController FindBottleAnimController(string recipeType)
    {
        var bottleData = draggableItemScript.ItemData as BottleData;
        return bottleAnimOverrides.FirstOrDefault(controller => controller.name == $"{recipeType}_{bottleData.BottleID}");
    }
}