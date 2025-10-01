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
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        spriteRend.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (GameManager.instance.State != GameManager.GameState.Playing) return;

        spriteRend.color = Color.white;
    }
}