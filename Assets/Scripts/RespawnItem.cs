using UnityEngine;

[RequireComponent(typeof(DraggableItem))]
public class RespawnItem : MonoBehaviour
{
    // --- Scripts ---
    private DraggableItem draggableItemScript;

    private Rigidbody2D rb2D;

    void Awake()
    {
        draggableItemScript = GetComponent<DraggableItem>();    
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            rb2D.bodyType = RigidbodyType2D.Static;
            transform.position = draggableItemScript.StartPosition;
        }
    }
}