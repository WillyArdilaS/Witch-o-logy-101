using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropArea), typeof(CauldronUI))]
public class IngredientContainer : MonoBehaviour
{
    // === Scripts===
    private DropArea dropAreaScript;
    private CauldronUI cauldronUIScript;

    // === Ingredients ===
    [SerializeField] private int ingredientsLimit;
    [SerializeField] private List<IngredientData> ingredientList = new();

    // === Properties ===
    public List<IngredientData> IngredientList => ingredientList;

    void Awake()
    {
        dropAreaScript = GetComponent<DropArea>();
        cauldronUIScript = GetComponent<CauldronUI>();

        dropAreaScript.IngredientDropped += AddIngredient;
    }

    private void AddIngredient(DraggableItem ingredient)
    {
        if (ingredientList.Count < ingredientsLimit)
        {
            ingredientList.Add(ingredient.ItemData as IngredientData);
            cauldronUIScript.ShowIngredientLabelUI(ingredient.ItemData as IngredientData);
        }
    }

    public void EmptyContainer()
    {
        ingredientList.Clear();
        cauldronUIScript.HideLabelUI();
    }
}