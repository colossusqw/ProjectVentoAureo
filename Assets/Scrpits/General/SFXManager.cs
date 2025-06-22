using UnityEngine;

public enum SFX
{
    MenuButtonHover,
    MenuStartGame,
    MenuOptionSelected,
    MenuReturn,
}


public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [SerializeField] private AudioSource defaultAudioSource;

    [SerializeField] private AudioClip menuButtonHover;
    [SerializeField] private AudioClip menuStartGame;
    [SerializeField] private AudioClip menuOptionSelected;
    [SerializeField] private AudioClip menuReturn;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlaySFX(SFX sfx, AudioSource source = null)
    {
        AudioClip clip = GetClip(sfx);
        if (clip == null) return;

        AudioSource finalSource = source;

        if (finalSource == null)
        {
            finalSource = defaultAudioSource;
        }

        if (finalSource != null && clip != null)
        {
            finalSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetClip(SFX sfx)
    {
        return sfx switch
        {
            SFX.MenuButtonHover => menuButtonHover,
            SFX.MenuStartGame => menuStartGame,
            SFX.MenuOptionSelected => menuOptionSelected,
            SFX.MenuReturn => menuReturn,
            _ => null,
        };
    }
}
