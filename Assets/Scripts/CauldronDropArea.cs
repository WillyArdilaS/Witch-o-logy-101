using UnityEngine;

public class CauldronDropArea : MonoBehaviour, IDropArea
{
    public void OnItemDrop(DraggableItem item)
    {
        Debug.Log("Se ha agregado: " + item.gameObject.name);
        item.gameObject.SetActive(false);
    }
}