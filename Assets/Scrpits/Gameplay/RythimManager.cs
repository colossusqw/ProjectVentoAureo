using UnityEngine;
using TMPro;

public class RythimManager : MonoBehaviour
{
    public TMP_Text textPoints;

    public RythimButton ButtonA;
    public RythimButton ButtonB;

    public bool playing = false;
    public float targetvel = 5f;

    public float GreatDist = 0.1f;
    public float GoodDist = 0.3f;
    public float OkDist = 0.6f;
    public float BadDist = 1.2f;

    public int points = 0;

    void Awake()
    {
        Invoke("StartRound", 6f);
    }

    public void UpdatePointText()
    {
        textPoints.text = "Score: " + points.ToString();
    }

    void StartRound() 
    {
        playing = true;

        InvokeRepeating("LaunchMusicNotes", 0f, 0.3f);
    }

    void LaunchMusicNotes()
    {
        bool randomChoice = Random.value < 0.5f;
        
        if (randomChoice)
        {
            if (!ButtonA.targetActive) ButtonA.targetActive = true;
        }
        else
        {
            if (!ButtonB.targetActive) ButtonB.targetActive = true;
        }
    }
}
