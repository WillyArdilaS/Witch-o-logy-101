using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "ScriptableObjects/Ingredient")]
public class IngredientData : ItemData
{
    // === Extra data fields ===
    public enum IngredientType { Bones, Essence_01, Essence_02, Essence_03, Eyes, Flowers, Radish, Rosemary, Salt, Wings }
    [SerializeField] private IngredientType ingredientID;

    // === Properties ===
    public IngredientType IngredientID => ingredientID;
}