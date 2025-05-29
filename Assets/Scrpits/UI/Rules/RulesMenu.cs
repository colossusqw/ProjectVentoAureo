using UnityEngine;

public class RulesMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.GM.ChangeScene("Level");
        }
    }
}
