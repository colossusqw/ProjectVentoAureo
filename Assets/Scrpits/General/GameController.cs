using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    [SerializeField] private Image FadeScreen;

    public string currentScene = "";
    public string sceneToChange = "";

    public bool Fade = false;

    void Awake()
    {
        GM = this;
        ChangeScene("Menu");
    }

    void Update()
    {
        HandleFade();
    }

    public void OffFade()
    {
        Fade = false;
    }

    void HandleFade()
    {
        var tempColor = FadeScreen.color;

        if (tempColor.a < 0.05f) FadeScreen.enabled = false;
        else FadeScreen.enabled = true;

        if (Fade && tempColor.a < 1f)
        {
            tempColor.a = Mathf.Min(tempColor.a + 0.5f * Time.deltaTime, 1f);
            FadeScreen.color = tempColor;
        }
        else if (!Fade && tempColor.a > 0f)
        {
            tempColor.a = Mathf.Max(tempColor.a - 0.5f * Time.deltaTime, 0f);
            FadeScreen.color = tempColor;
        }
    }

    public void ChangeScene(string newScene, bool doFade = true)
    {
        if (doFade)Fade = true;

        sceneToChange = newScene;

        Invoke("DoTheSceneChange", 2f);
    }

    public void DoTheSceneChange()
    {
        if (currentScene != "") SceneManager.UnloadSceneAsync(currentScene);
        currentScene = sceneToChange;
        SceneManager.LoadScene(sceneToChange, LoadSceneMode.Additive);

        Invoke("OffFade", 2f);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
