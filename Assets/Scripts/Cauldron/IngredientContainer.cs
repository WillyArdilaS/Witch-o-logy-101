using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropArea))]
public class IngredientContainer : MonoBehaviour
{
    // === Scripts===
    private DropArea dropAreaScript;

    // === Ingredients ===
    [SerializeField] private int ingredientsLimit;
    [SerializeField] private List<IngredientData> ingredientList = new();

    // === Properties ===
    public List<IngredientData> IngredientList => ingredientList;

    void Awake()
    {
        dropAreaScript = GetComponent<DropArea>();

        dropAreaScript.IngredientDropped += AddIngredient;
    }

    private void AddIngredient(DraggableItem ingredient)
    {
        if (ingredientList.Count < ingredientsLimit)
        {
            ingredientList.Add(ingredient.ItemData as IngredientData);
        }
    }

    public void EmptyContainer()
    {
        ingredientList.Clear();
    }
}