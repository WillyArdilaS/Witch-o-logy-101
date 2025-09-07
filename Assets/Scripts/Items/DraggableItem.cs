using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour
{
    // === Item ===
    [SerializeField] private ItemData itemData;
    [SerializeField] private bool isVisible = true;

    // === Drag movement ===
    private Rigidbody2D rb2D;

    // === Order in layer ===
    [SerializeField] private int layerNumber;
    private SpriteRenderer spriteRend;

    // === Properties ===
    public ItemData ItemData => itemData;
    public bool IsVisible => isVisible;
    public Rigidbody2D RB2D => rb2D;

    protected virtual void Awake()
    {
        itemData.StartPosition = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        spriteRend.sortingOrder = layerNumber; // Reset order in layer
    }

    void OnMouseDown()
    {
        rb2D.bodyType = RigidbodyType2D.Static;
        spriteRend.sortingOrder = 4;
        itemData.ShowData();
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorld();
    }

    void OnMouseUp()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(GetMousePositionInWorld());

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject) continue; // Skip the element itself

            if (hit.TryGetComponent(out DropArea dropArea))
            {
                dropArea.OnItemDrop(this);
                return;
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