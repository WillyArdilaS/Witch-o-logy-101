using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverEffect : MonoBehaviour
{
    // === Hover ===
    [SerializeField] private Color hoverColor;
    private SpriteRenderer spriteRend;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        spriteRend.color = hoverColor;
    }

    void OnMouseExit()
    {
        spriteRend.color = Color.white;
    }
}