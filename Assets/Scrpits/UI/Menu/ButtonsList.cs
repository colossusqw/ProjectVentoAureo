using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsList : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] private Slider volumeSlider;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.8f);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
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
        GameManager.GM.ChangeScene("Rules");
    }
    
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
        AudioListener.volume = volume;
    }
}
