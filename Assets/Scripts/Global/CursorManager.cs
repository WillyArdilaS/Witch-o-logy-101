using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    // === Singleton ===
    public static CursorManager instance;

    // === New cursor ===
    [SerializeField] private RectTransform cursorUI;
    [SerializeField] Sprite defaultCursor;
    [SerializeField] Sprite clickCursor;
    private Image cursorImage;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

     void Start()
    {
        cursorImage = cursorUI.GetComponent<Image>();
        cursorImage.sprite = defaultCursor;
    }

    void Update()
    {
        // Move cursor
        cursorUI.position = Input.mousePosition;

        // Change the cursor sprite when the left click is held down
        if (Input.GetMouseButton(0))
        {
            cursorImage.sprite = clickCursor;
        }
        else
        {
            cursorImage.sprite = defaultCursor;
        }
    }
}