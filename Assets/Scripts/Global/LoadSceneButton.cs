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

        buttonUI.onClick.AddListener(PlaySFX);
        buttonUI.onClick.AddListener(ChangeScene);
    }

    private void PlaySFX()
    {
        GlobalGameManager.instance.AudioManager.PlayUISFX(AudioManager.UISfxType.Button, 0, GlobalGameManager.instance.AudioManager.ButtonClickVol);
    }

    private void ChangeScene()
    {
        GlobalGameManager.instance.SceneSwitchManager.LoadScene(sceneName);
    } 
}