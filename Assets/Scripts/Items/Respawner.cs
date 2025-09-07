using System.Collections;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    // === Scripts ===
    private DraggableItem draggableItemScript;
    private DeadZoneDetector detectDeadZoneScript;
    private DropArea dropAreaScript;

    // === Item ===
    private GameObject itemToRespawn;

    // === Coroutines ===
    private Coroutine cooldownRoutine;

    void Awake()
    {
        draggableItemScript = GetComponentInChildren<DraggableItem>();
        detectDeadZoneScript = GetComponentInChildren<DeadZoneDetector>();

        detectDeadZoneScript.OnDeadZone += StartCooldown;

        if (transform.childCount > 0)
        {
            itemToRespawn = transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogWarning("Este respawner no tiene ningun item asociado");
        }
    }

    public void SubscribeToDropAreaEvent(DropArea dropArea)
    {
        dropAreaScript = dropArea;
        dropAreaScript.OnDropArea += StartCooldown; 
    }

    public void StartCooldown()
    {
        if (cooldownRoutine != null) StopCoroutine(cooldownRoutine);
        cooldownRoutine = StartCoroutine(CooldownForRespawn());
    }

    private IEnumerator CooldownForRespawn()
    {
        yield return new WaitForSeconds(draggableItemScript.ItemData.RespawnTime);
        Respawn();
    }

    private void Respawn()
    {

        itemToRespawn.transform.position = draggableItemScript.ItemData.StartPosition;
        draggableItemScript.ResetItem();
    }
}