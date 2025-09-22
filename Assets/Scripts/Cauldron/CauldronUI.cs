using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronUI : MonoBehaviour
{
    // === UI containers ===
    [SerializeField] private List<GameObject> labelSpaces;

    // === Instantiation ===
    [SerializeField] private GameObject labelPrefab;
    private GameObject labelUI;
    private List<GameObject> labeUIList = new();

    public void ShowIngredientLabelUI(IngredientData ingredient)
    {
        foreach (var space in labelSpaces)
        {
            if (space.transform.childCount != 0) continue;

            labelUI = Instantiate(labelPrefab, space.transform);
            labelUI.GetComponent<Image>().sprite = ingredient.LabelImage;
            labeUIList.Add(labelUI);
            return;
        }
    }

    public void HideLabelUI()
    {
        foreach (var labelUI in labeUIList)
        {
            Destroy(labelUI);
        }

        labeUIList.Clear();
    }
}