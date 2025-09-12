using System;
using UnityEngine;

public class DeadZoneDetector : MonoBehaviour
{
    // === Events ===
    public event Action DeadZoneEntered;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            DeadZoneEntered?.Invoke();
        }
    }
}