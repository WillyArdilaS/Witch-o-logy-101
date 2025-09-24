using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneButton : MonoBehaviour
{
    // === Scene settings ===
    [SerializeField] private string sceneName;

    // === UI ===
    private Button buttonUI;

    void OnEnable()
    {
        buttonUI = GetComponent<Button>();

        if (GlobalGameManager.instance != null)
        {
            buttonUI.onClick.AddListener(ChangeScene);
        }
    }

    private void ChangeScene()
    {
        GlobalGameManager.instance.SceneSwitchManager.LoadScene(sceneName);
    }
}