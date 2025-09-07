using System;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    // === Events ===
    public event Action OnDropArea;

    public void OnItemDrop(DraggableItem item)
    {
        item.GetComponentInParent<Respawner>().SubscribeToDropAreaEvent(this);

        Debug.Log("Se ha agregado: " + item.ItemData.ItemName);
        item.gameObject.SetActive(false);
        OnDropArea?.Invoke();
    }
}