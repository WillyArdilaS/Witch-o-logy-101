using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // === UI hover ===
    [SerializeField] private Color hoverColor;
    private Image imageUI;

    void Awake()
    {
        imageUI = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageUI.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageUI.color = Color.white;
    }
}