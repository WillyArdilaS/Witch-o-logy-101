using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropArea))]
public class IngredientContainer : MonoBehaviour
{
    // === Scripts===
    private DropArea dropAreaScript;

    // === Ingredients ===
    [SerializeField] private List<DraggableItem> ingredientList;

    // === Properties ===
    public List<DraggableItem> IngredientList => ingredientList;

    void Awake()
    {
        dropAreaScript = GetComponent<DropArea>();

        dropAreaScript.IngredientDropped += AddIngredient;
    }

    private void AddIngredient(DraggableItem ingredient)
    {
        ingredientList.Add(ingredient);
    }

    public void EmptyContainer()
    {
        ingredientList.Clear();
    }
}