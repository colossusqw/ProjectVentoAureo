using UnityEngine;

public class InitialDialogueManager : MonoBehaviour
{
    public GameObject dialogBoxSystem;
    public GameObject orderBoxSystem;
    public DialogData initialDialogData;
    public string nextSceneName;

    private void OnEnable()
    {
        Invoke("StartInitialDialog", 0.5f);
    }

    private void StartInitialDialog()
    {
        dialogBoxSystem.SetActive(true);
        dialogBoxSystem.GetComponent<DialogBoxSystem>().StartDialog(initialDialogData, OnDialogEnd);
    }

    private void OnDialogEnd()
    {
        orderBoxSystem.SetActive(true);
        orderBoxSystem.GetComponent<OrderBoxSystem>().StartOrderBox(OnOrderEnd);
    }

    private void OnOrderEnd()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            MusicManager.Instance.StopMusic();
            GameManager.GM.ChangeScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Scene Name dont have been defined!");
        }
    }
}
