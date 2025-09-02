using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    // --- Drag movement ---
    private Rigidbody2D rb2D;
    private Vector2 startPosition;

    // --- Properties ---
    public Vector2 StartPosition => startPosition;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
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

            if (hit.TryGetComponent<IDropArea>(out IDropArea dropArea))
            {
                dropArea.OnItemDrop(this);
                return;
            }
        }

        // If the item is not located in any valid drop zone, it falls
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private Vector2 GetMousePositionInWorld()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
}