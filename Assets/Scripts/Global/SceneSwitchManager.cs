using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
            switch (sceneName)
            {
                case "MainMenu":
                    if (GlobalGameManager.instance.AudioManager.MusicSource.clip.name != "MainMenu") // To avoid restarting the music if it is already playing
                    {
                        GlobalGameManager.instance.AudioManager.PlayMusic(AudioManager.MusicType.MainMenu, 0, GlobalGameManager.instance.AudioManager.MainMenuSongVol);
                    }
                    break;
                case "GameScene":
                    GlobalGameManager.instance.AudioManager.PlayMusic(AudioManager.MusicType.InGame, 0, GlobalGameManager.instance.AudioManager.GameSongVol);
                    break;
                case "GameOver":
                    GlobalGameManager.instance.AudioManager.StopMusic();
                    break;
            }

        SceneManager.LoadScene(sceneName);
    }
}