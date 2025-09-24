using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recibe Book", menuName = "ScriptableObjects/RecipeBook")]
public class RecipeBookData : ScriptableObject
{
    // === Data fields ===
    [SerializeField] private List<RecipeData> recipeList = new();

    // === Properties ===
    public List<RecipeData> RecipeList => recipeList;

    public RecipeData GetRandomRecipe()
    {
        if (recipeList.Count > 0)
        {
            int randomIndex = Random.Range(0, recipeList.Count);
            return recipeList[randomIndex];
        }

        return null;
    }
}