using UnityEngine;

public class ItemData : ScriptableObject
{
    // === Data fields ===
    public enum ItemType { Ingredient, Bottle }
    [SerializeField] private string itemName;
    [SerializeField] private ItemType type;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private float respawnTime;

    // === Properties ===
    public string ItemName => itemName;
    public ItemType Type => type;
    public Vector2 StartPosition  { get => startPosition; set => startPosition = value; }
    public float RespawnTime => respawnTime;
}