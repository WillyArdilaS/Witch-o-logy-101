using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour
{
    // === Item ===
    [SerializeField] private ItemData itemData;

    // === Drag movement ===
    private Rigidbody2D rb2D;    

    // === Order in layer ===
    [SerializeField] private int newLayerNumber;
    private SpriteRenderer spriteRend;
    private int originalLayerNumber;

    // === Properties ===
    public ItemData ItemData => itemData;

    protected virtual void Awake()
    {
        itemData.StartPosition = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        originalLayerNumber = spriteRend.sortingOrder;
    }

    public void ResetItem()
    {
        spriteRend.sortingOrder = originalLayerNumber; // Reset order in layer
        rb2D.bodyType = RigidbodyType2D.Static;
        gameObject.SetActive(true);
    }

    void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        rb2D.bodyType = RigidbodyType2D.Static;
        spriteRend.sortingOrder = newLayerNumber;
    }    

    void OnMouseDrag()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        transform.position = GetMousePositionInWorld();
    }

    void OnMouseUp()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        
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