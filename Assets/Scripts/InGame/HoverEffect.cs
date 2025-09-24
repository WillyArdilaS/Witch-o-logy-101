using UnityEngine;
using UnityEngine.EventSystems;

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
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        spriteRend.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        spriteRend.color = Color.white;
    }
}