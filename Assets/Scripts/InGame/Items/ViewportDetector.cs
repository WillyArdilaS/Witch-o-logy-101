using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(DraggableItem))]
public class ViewportDetector : MonoBehaviour
{
    // === Item ===
    private SpriteRenderer spriteRenderer;
    private readonly Vector3[] spriteCorners = new Vector3[4];
    private ItemData.ItemType itemType;

    // === Camera ===
    private Camera mainCam;
    private bool isOutOfView = false;

    // === Events ===
    public event Action ViewportExit;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemType = GetComponent<DraggableItem>().ItemData.Type;
        mainCam = Camera.main;
    }

    void Update()
    {
        // Calculating sprite corners in viewport
        Bounds bounds = spriteRenderer.bounds;
        
        spriteCorners[0] = mainCam.WorldToViewportPoint(new Vector3(bounds.min.x, bounds.min.y)); // bottom-left
        spriteCorners[1] = mainCam.WorldToViewportPoint(new Vector3(bounds.min.x, bounds.max.y)); // top-left
        spriteCorners[2] = mainCam.WorldToViewportPoint(new Vector3(bounds.max.x, bounds.min.y)); // bottom-right
        spriteCorners[3] = mainCam.WorldToViewportPoint(new Vector3(bounds.max.x, bounds.max.y)); // top-right

        // Check if ANY corner is still within the viewport
        bool isAnyInside = false;
        foreach (var corner in spriteCorners)
        {
            if (corner.z > 0 && corner.x >= 0 && corner.x <= 1 && corner.y >= 0 && corner.y <= 1) // corner.z > 0 to validate that it's in front of the camera
            {
                isAnyInside = true;
                break;
            }
        }

        // If none of the corners are inside, it means that it is completely outside
        if (!isAnyInside && !isOutOfView)
        {
            isOutOfView = true;

            // Validate the item type to play the corresponding sound
            if (itemType == ItemData.ItemType.Bottle)
            {
                GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Item, 0, GlobalGameManager.instance.AudioManager.BottleFallVol);
            }
            else if (itemType == ItemData.ItemType.Ingredient)
            {
                GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Item, 1, GlobalGameManager.instance.AudioManager.IngredientFallVol);
            }

            ViewportExit?.Invoke();
        }

        // Reset the value if the object returns to the viewport
        if (isAnyInside && isOutOfView)
        {
            isOutOfView = false;
        }
    }
}