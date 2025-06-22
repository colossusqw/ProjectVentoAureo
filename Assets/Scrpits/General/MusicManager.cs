using UnityEngine;

public enum MUSIC
{
    MainMenuTheme,
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource defaultAudioSource;

    [SerializeField] private AudioClip mainMenuTheme;

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
            finalSource.clip = mainMenuTheme;
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
            _ => null,
        };
    }
}


