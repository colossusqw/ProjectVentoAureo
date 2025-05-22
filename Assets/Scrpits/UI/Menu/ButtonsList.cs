using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsList : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        GameManager.GM.ChangeScene("Level");
    }
}
