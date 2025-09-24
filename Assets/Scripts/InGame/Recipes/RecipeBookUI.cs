using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{
    // === Recipe book ===
    [SerializeField] private GameObject recipeBookUI;

    void OnMouseDown()
    {
        ShowRecipeBookUI();
    }

    private void ShowRecipeBookUI()
    {
        recipeBookUI.SetActive(true);
    }

    public void HideRecipeBookUI()
    {
        recipeBookUI.SetActive(false);  
    }
}