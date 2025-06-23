using UnityEngine;

public enum MUSIC
{
    MainMenuTheme,
    CafeAmbienceTheme
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private AudioSource defaultAudioSource;

    [Header("Musics Menu")]
    [SerializeField] private AudioClip mainMenuTheme;

    [Header("Musics Cutscene")]
    [SerializeField] private AudioClip cafeAmbienceTheme;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayMusic(MUSIC music, AudioSource source = null)
    {
        AudioClip clip = GetClip(music);
        if (clip == null) return;

        AudioSource finalSource = source;

        if (finalSource == null)
        {
            finalSource = defaultAudioSource;
        }

        if (finalSource != null && clip != null)
        {
            finalSource.Stop();
            finalSource.loop = true;
            finalSource.clip = clip;
            finalSource.Play();
        }
    }

    public void StopMusic(AudioSource source = null)
    {
        AudioSource finalSource = source;

        if (finalSource == null)
        {
            finalSource = defaultAudioSource;
        }

        if (finalSource != null)
        {

            finalSource.Stop();
        }
    }

    private AudioClip GetClip(MUSIC music)
    {
        return music switch
        {
            MUSIC.MainMenuTheme => mainMenuTheme,
            MUSIC.CafeAmbienceTheme => cafeAmbienceTheme,
            _ => null,
        };
    }
}


