using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverEffect : MonoBehaviour
{
    // === Hover ===
    [SerializeField] private Color hoverColor;
    private SpriteRenderer spriteRend;

    protected virtual void Awake()
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