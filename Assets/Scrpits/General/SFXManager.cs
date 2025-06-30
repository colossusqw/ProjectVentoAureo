using UnityEngine;

public enum SFX
{
    //SFXs Menu
    MenuButtonHover,
    MenuStartGame,
    MenuOptionSelected,
    MenuReturn,

    //SFXs Cutscene
    CostumerEntering,
    DialogBoxPop,
    Results,

    //SFXs Gameplay
    HitSchifoso,
    HitBene,
    HitGrande,
    HitEccellente
}


public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private AudioSource defaultAudioSource;

    [Header("SFXs Menu")]
    [SerializeField] private AudioClip menuButtonHover;
    [SerializeField] private AudioClip menuStartGame;
    [SerializeField] private AudioClip menuOptionSelected;
    [SerializeField] private AudioClip menuReturn;

    [Header("SFXs Cutscenes")]
    [SerializeField] private AudioClip costumerEntering;
    [SerializeField] private AudioClip dialogBoxPop;
    [SerializeField] private AudioClip results;

    [Header("SFXs Gameplay")]
    [SerializeField] private AudioClip hitSchifoso;
    [SerializeField] private AudioClip hitBene;
    [SerializeField] private AudioClip hitGrande;
    [SerializeField] private AudioClip hitEccellente;

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
            //SFXs Menu
            SFX.MenuButtonHover => menuButtonHover,
            SFX.MenuStartGame => menuStartGame,
            SFX.MenuOptionSelected => menuOptionSelected,
            SFX.MenuReturn => menuReturn,

            //SFXs Cutscene
            SFX.CostumerEntering => costumerEntering,
            SFX.DialogBoxPop => dialogBoxPop,
            SFX.Results => results,

            //SFXs Gameplay
            SFX.HitSchifoso => hitSchifoso,
            SFX.HitBene => hitBene,
            SFX.HitGrande => hitGrande,
            SFX.HitEccellente => hitEccellente,
            _ => null,
        };
    }
}
