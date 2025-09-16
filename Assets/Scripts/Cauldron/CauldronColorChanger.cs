using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(DropArea), typeof(IngredientContainer))]
public class CauldronColorChanger : MonoBehaviour
{
    // === Scripts ===
    private DropArea dropAreaScript;
    private IngredientContainer ingredientContainerScript;

    // === Recipes data ===
    [SerializeField] private RecipeBook recipeBook;
    private RecipeData matchingRecipe;

    // === Cauldron sprite ===
    [SerializeField] private Sprite[] cauldronImages;
    private SpriteRenderer spriteRend;

    // === Properties ===
    public RecipeData MatchingRecipe { get => matchingRecipe; set => matchingRecipe = value; }

    void Awake()
    {
        dropAreaScript = GetComponent<DropArea>();
        ingredientContainerScript = GetComponent<IngredientContainer>();

        dropAreaScript.IngredientDropped += _ => ChangeColor();
        dropAreaScript.BottleDropped += _ => ResetColor();

        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnDestroy()
    {
        dropAreaScript.IngredientDropped -= _ => ChangeColor();
        dropAreaScript.BottleDropped -= _ => ResetColor();
    }

    private void ChangeColor()
    {
        if (ingredientContainerScript.IngredientList.Count < 3)
        {
            spriteRend.sprite = FindCauldronImg("Progress");
        }
        else
        {
            // Show correct or wrong depending on match
            spriteRend.sprite = FindCauldronImg(CompareIngredients());
        }
    }

    private void ResetColor()
    {
        spriteRend.sprite = FindCauldronImg("Empty");
    }

    private string CompareIngredients()
    {
        List<IngredientData> ingredientsInCauldron = ingredientContainerScript.IngredientList;

        matchingRecipe = recipeBook.RecipeList.FirstOrDefault(recipe => recipe.RequiredIngredients.All(ingredient => ingredientsInCauldron.Contains(ingredient)));

        if (matchingRecipe != null)
        {
            return matchingRecipe.RecipeID.ToString();
        }
        else
        {
            return "Wrong";
        }
    }

    private Sprite FindCauldronImg(string cauldronType)
    {

        return cauldronImages.FirstOrDefault(img => img.name == $"{cauldronType}Cauldron");
    }
}