using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronUI : MonoBehaviour
{
    // === UI containers ===
    [SerializeField] private List<GameObject> labelContainers;

    // === Instantiation ===
    [SerializeField] private GameObject labelPrefab;
    private GameObject labelUI;
    private List<GameObject> labeUIList = new();

    public void ShowIngredientLabelUI(IngredientData ingredient)
    {
        foreach (var container in labelContainers)
        {
            if (container.transform.childCount != 0) continue;

            labelUI = Instantiate(labelPrefab, container.transform);
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