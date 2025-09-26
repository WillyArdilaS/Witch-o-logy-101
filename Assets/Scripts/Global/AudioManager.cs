using System;
using UnityEngine;

// === Structures ===
[Serializable]
public struct MusicList
{
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] songs;
    public AudioClip[] Songs => songs;
}

[Serializable]
public struct SfxList
{
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sfx;
    public AudioClip[] Sfx => sfx;
}

[Serializable]
public struct UISfxList
{
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] uiSfx;
    public AudioClip[] UISfx => uiSfx;
}

// === Class ===
[ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    // === Audio source ===
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSfxSource;

    // === Music ===
    [SerializeField] private MusicList[] musicList;
    [HideInInspector] public enum MusicType { MainMenu, InGame }

    // === Sfx ===
    [SerializeField] private SfxList[] sfxList;
    [HideInInspector] public enum SfxType { General, Item, Order, Scroll }

    // === UI Sfx ===
    [SerializeField] private UISfxList[] uiSfxList;

    // === Volumes ===
    [Header("Music Volumes")]
    [SerializeField, Range(0, 1)] private float mainMenuSongVol;
    [SerializeField, Range(0, 1)] private float gameSongVol;

    [Header("SFX Volumes")]
    [SerializeField, Range(0, 1)] private float recipeBookVol;
    [SerializeField, Range(0, 1)] private float bottleFallVol;
    [SerializeField, Range(0, 1)] private float ingredientFallVol;
    [SerializeField, Range(0, 1)] private float cauldronDropVol;
    [SerializeField, Range(0, 1)] private float newOrderVol;
    [SerializeField, Range(0, 1)] private float deliverOrderVol;
    [SerializeField, Range(0, 1)] private float correctOrderVol;
    [SerializeField, Range(0, 1)] private float wrongOrderVol;
    [SerializeField, Range(0, 1)] private float anyScrollBurntVol;
    [SerializeField, Range(0, 1)] private float lastScrollBurntVol;
    [SerializeField, Range(0, 1)] private float successfulDayVol;

    [Header("UI SFX Volumes")]
    [SerializeField, Range(0, 1)] private float buttonClickVol;

    // === Properties ===
    public float MainMenuSongVol => mainMenuSongVol;
    public float GameSongVol => gameSongVol;
    public float RecipeBookVol => recipeBookVol;
    public float BottleFallVol => bottleFallVol;
    public float IngredientFallVol => ingredientFallVol;
    public float CauldronDropVol => cauldronDropVol;
    public float NewOrderVol => newOrderVol;
    public float DeliverOrderVol => deliverOrderVol;
    public float CorrectOrderVol => correctOrderVol;
    public float WrongOrderVol => wrongOrderVol;
    public float AnyScrollBurntVol => anyScrollBurntVol;
    public float LastScrollBurntVol => lastScrollBurntVol;
    public float SuccessfulDayVol => successfulDayVol;
    public float ButtonClickVol => buttonClickVol;

    public void PlayMusic(MusicType musicType, int musicIndex, float volume = 1)
    {
        AudioClip[] songs = musicList[(int)musicType].Songs;
        musicSource.clip = songs[musicIndex];
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // === Synchronization of list arrays with enums (only in the editor) ===
#if UNITY_EDITOR
    void OnEnable()
    {
        // For music
        string[] musicNames = Enum.GetNames(typeof(MusicType)); // Get the names of all values in the MusicType enum
        Array.Resize(ref musicList, musicNames.Length);

        for (int i = 0; i < musicList.Length; i++)
        {
            musicList[i].name = musicNames[i];
        }

        // For SFX
        string[] sfxNames = Enum.GetNames(typeof(SfxType)); // Get the names of all values in the SfxType enum
        Array.Resize(ref sfxList, sfxNames.Length);

        for (int i = 0; i < sfxList.Length; i++)
        {
            sfxList[i].name = sfxNames[i];
        }
    }
#endif
}