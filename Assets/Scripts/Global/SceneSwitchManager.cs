using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // if (GlobalGameManager.instance.AudioManager.MusicSource.clip != GlobalGameManager.instance.AudioManager.GetMusicClip()) // To avoid restarting the music if it is already playing
        // {
            switch (sceneName)
            {
                case "MainMenu":
                    GlobalGameManager.instance.AudioManager.PlayMusic(AudioManager.MusicType.MainMenu, 0, GlobalGameManager.instance.AudioManager.MainMenuSongVol);
                    break;
                case "GameScene":
                    GlobalGameManager.instance.AudioManager.PlayMusic(AudioManager.MusicType.InGame, 0, GlobalGameManager.instance.AudioManager.GameSongVol);
                    break;
                case "GameOver":
                    GlobalGameManager.instance.AudioManager.StopMusic();
                    break;
            }
        // }

        GlobalGameManager.instance.AudioManager.PlayUISFX(AudioManager.UISfxType.Button, 0, GlobalGameManager.instance.AudioManager.ButtonClickVol);
        SceneManager.LoadScene(sceneName);
    }
}