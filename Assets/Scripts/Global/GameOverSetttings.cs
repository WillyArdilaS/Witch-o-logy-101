using System.Collections.Generic;
using UnityEngine;

public class GameOverSettings : MonoBehaviour
{
    // === UI ===
    private Dictionary<int, GameObject> starMenus = new();

    void Awake()
    {
        // Fill the dictionary with the children of the canvas
        for (int i = 0; i < transform.childCount; i++)
        {
            starMenus[i] = transform.GetChild(i).gameObject;
        }

        ShowMenu();
    }

    private void ShowMenu()
    {
        int stars = GameManager.starsEarned;

        foreach (var menu in starMenus.Values)
        {
            menu.SetActive(false);
        }

        if (starMenus.ContainsKey(stars)) starMenus[stars].SetActive(true);
    }
}