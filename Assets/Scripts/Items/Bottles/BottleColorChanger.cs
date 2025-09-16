using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DraggableItem), typeof(SpriteRenderer))]
public class BottleColorChanger : MonoBehaviour
{
    // Scripts
    private DraggableItem draggableItemScript;

    // === Bottle sprite ===
    [SerializeField] private Sprite[] bottleImages;
    private SpriteRenderer spriteRend;

    void Awake()
    {
        draggableItemScript = GetComponent<DraggableItem>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(GameObject cauldron)
    {
        RecipeData recipeInCauldron = cauldron.GetComponent<CauldronColorChanger>().MatchingRecipe;

        if (recipeInCauldron != null)
        {
            // Change color depending on the match in the cauldron
            spriteRend.sprite = FindBottleImg(recipeInCauldron.RecipeID.ToString());
        }
        else
        {
            spriteRend.sprite = FindBottleImg("Wrong");
        }

        // Nullify the matching recipe in the cauldron to restore its color
        cauldron.GetComponent<CauldronColorChanger>().MatchingRecipe = null;
    }

    public void ResetColor()
    {
        spriteRend.sprite = FindBottleImg("Empty");
    }

    private Sprite FindBottleImg(string recipeType)
    {
        var bottleData = draggableItemScript.ItemData as BottleData;
        return bottleImages.FirstOrDefault(img => img.name == $"{recipeType}_{bottleData.BottleID}");
    }
}