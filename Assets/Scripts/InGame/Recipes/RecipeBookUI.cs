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
        GameManager.instance.State = GameManager.GameState.ShowingRecipeBook;
    }

    public void HideRecipeBookUI()
    {
        recipeBookUI.SetActive(false);  
        GameManager.instance.State = GameManager.GameState.Playing;
    }
}