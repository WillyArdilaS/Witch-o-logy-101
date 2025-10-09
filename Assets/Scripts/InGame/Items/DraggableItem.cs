using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class DraggableItem : MonoBehaviour
{
    // === Item ===
    [SerializeField] private ItemData itemData;

    // === Drag movement ===
    private Rigidbody2D rb2D;    

    // === Sorting layer ===
    private SpriteRenderer spriteRend;
    private string newSortingLayerName = "DraggedItem";
    private string originalSortingLayerName;

    // === Properties ===
    public ItemData ItemData => itemData;

    protected virtual void Awake()
    {
        itemData.StartPosition = transform.position;

        rb2D = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        
        originalSortingLayerName = spriteRend.sortingLayerName;
    }

    public void ResetItem()
    {
        spriteRend.sortingLayerName = originalSortingLayerName; // Reset sorting layer
        rb2D.bodyType = RigidbodyType2D.Static;
        gameObject.SetActive(true);
    }

    void OnMouseDown()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        rb2D.bodyType = RigidbodyType2D.Static;
        spriteRend.sortingLayerName = newSortingLayerName;
    }    

    void OnMouseDrag()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        transform.position = GetMousePositionInWorld();
    }

    void OnMouseUp()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;
        
        Collider2D[] hits = Physics2D.OverlapPointAll(GetMousePositionInWorld());

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject) continue; // Skip the element itself

            if (hit.TryGetComponent(out DropArea dropArea) && hit.TryGetComponent(out IngredientContainer ingredientContainer))
            {
                if (itemData.Type == ItemData.ItemType.Ingredient && ingredientContainer.IngredientList.Count < 3)
                {
                    dropArea.OnItemDrop(this);
                    return;
                }
                else if(itemData.Type == ItemData.ItemType.Bottle)
                {
                    dropArea.OnItemDrop(this);
                    return;
                }
            }
        }

        // If the item is not located in any valid drop zone, it falls
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    protected Vector2 GetMousePositionInWorld()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
}