using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsList : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject RulesMenu;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private float fadeDuration;
    [SerializeField] private string StartGameScene;
    [SerializeField] private Image lightUI;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        MusicManager.Instance.PlayMusic(MUSIC.MainMenuTheme);

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.8f);
        sfxSlider.value   = PlayerPrefs.GetFloat("sfxVolume",   0.8f);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        RulesMenu.SetActive(false);

        StartCoroutine(MainMenuFadeIn());
    }

    public void LeaveGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OpeningRulesMenu()
    {
        StartCoroutine(StartGameAndFadeInOutAnimation());
    } 

    public void StartGame()
    {
        SFXManager.Instance.PlaySFX(SFX.MenuStartGame);
        MusicManager.Instance.StopMusic();

        GameManager.GM.ChangeScene(StartGameScene);
    }

    public void ButtonSelectedSFX()
    {
        SFXManager.Instance.PlaySFX(SFX.MenuOptionSelected);
    }

    public void ButtonReturn()
    {
        SFXManager.Instance.PlaySFX(SFX.MenuReturn);
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicParameter", Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20f);

        PlayerPrefs.SetFloat("musicVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFXParameter", Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20f);

        PlayerPrefs.SetFloat("sfxVolume", sliderValue);
        PlayerPrefs.Save();
    }

    private IEnumerator MainMenuFadeIn()
    {
        CanvasGroup mainMenuGroup = mainMenu.GetComponent<CanvasGroup>();

        float t = 0;
        mainMenuGroup.alpha = 0f;

        yield return new WaitForSeconds(fadeDuration);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            mainMenuGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        mainMenuGroup.alpha = 1f;
        mainMenuGroup.interactable = true;
    }

    private IEnumerator StartGameAndFadeInOutAnimation()
    {
        CanvasGroup rulesMenuGroup = RulesMenu.GetComponent<CanvasGroup>();

        float t = 0;
        rulesMenuGroup.alpha = 0;
        rulesMenuGroup.interactable = false;
        Color lightColor = lightUI.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            if (lightColor.a >= 1) t = fadeDuration;

            float lightAlpha = Mathf.Lerp(lightUI.color.a, 1f, t / fadeDuration);
            lightColor.a = lightAlpha;
            lightUI.color = lightColor;

            yield return null;
        }

        t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime * 2f;

            rulesMenuGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);

            yield return null;
        }

        rulesMenuGroup.interactable = true;
    }
}
