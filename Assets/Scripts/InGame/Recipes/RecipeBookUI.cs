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
        GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.General, 0, GlobalGameManager.instance.AudioManager.RecipeBookVol);
        
        recipeBookUI.SetActive(true);
        GameManager.instance.State = GameManager.GameState.ShowingRecipeBook;

    }

    public void HideRecipeBookUI()
    {
        recipeBookUI.SetActive(false);  
        GameManager.instance.State = GameManager.GameState.Playing;
    }
}