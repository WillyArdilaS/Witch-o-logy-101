using UnityEngine;

public class DropArea : MonoBehaviour
{
    public void OnItemDrop(DraggableItem item)
    {
        Debug.Log("Se ha agregado: " + item.ItemData.ItemName);
        item.gameObject.SetActive(false);
    }
}