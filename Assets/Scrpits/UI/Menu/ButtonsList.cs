using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsList : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject RulesMenu;
    [SerializeField] private Slider volumeSlider;
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

        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.8f);
        volumeSlider.onValueChanged.AddListener(SetVolume);

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

    public void StartGame()
    {
        SFXManager.Instance.PlaySFX(SFX.MenuStartGame);
        MusicManager.Instance.StopMusic();
        StartCoroutine(StartGameAndFadeInOutAnimation());
    }

    public void ButtonSelectedSFX()
    {
        SFXManager.Instance.PlaySFX(SFX.MenuOptionSelected);
    }

    public void ButtonReturn()
    {
        SFXManager.Instance.PlaySFX(SFX.MenuReturn);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
        AudioListener.volume = volume;
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
            rulesMenuGroup.alpha = Mathf.Lerp(1f, 0f, t*5f / fadeDuration);

            float lightAlpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            lightColor.a = lightAlpha;
            lightUI.color = lightColor;

            yield return null;
        }

        GameManager.GM.ChangeScene(StartGameScene);
    }
}
