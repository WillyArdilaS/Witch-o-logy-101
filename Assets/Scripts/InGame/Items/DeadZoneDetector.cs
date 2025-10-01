using System;
using UnityEngine;

public class DeadZoneDetector : MonoBehaviour
{
    // === Item type ===
    private ItemData.ItemType itemType;

    // === Events ===
    public event Action DeadZoneEntered;
    
    void Start()
    {
        itemType = gameObject.GetComponent<DraggableItem>().ItemData.Type;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            if (itemType == ItemData.ItemType.Bottle)
            {
                GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Item, 0, GlobalGameManager.instance.AudioManager.BottleFallVol);
            }
            else if (itemType == ItemData.ItemType.Ingredient)
            {
                GlobalGameManager.instance.AudioManager.PlaySFX(AudioManager.SfxType.Item, 1, GlobalGameManager.instance.AudioManager.IngredientFallVol);
            }
            
            DeadZoneEntered?.Invoke();
        }
    }
}