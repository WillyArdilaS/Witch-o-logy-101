using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeData : ScriptableObject
{
    // === Data fields ===
    public enum RecipeType { Beauty, Death, Electric, Frog, Intelligence, Love, Time }
    [SerializeField] private string recipeName;
    [SerializeField] private RecipeType recipeID;
    [SerializeField] private ItemData[] requiredIngredients = new ItemData[3];
    

    // === Properties ===
    public string RecipeName => recipeName;
    public RecipeType RecipeID => recipeID;
    public ItemData[] RequiredIngredients => requiredIngredients;
}