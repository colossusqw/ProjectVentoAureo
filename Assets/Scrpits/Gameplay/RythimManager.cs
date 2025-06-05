using UnityEngine;
using TMPro;

public class RythimManager : MonoBehaviour
{
    public TMP_Text textPoints;

    public RythimButton ButtonA;

    public GameObject Target;
    public GameObject Track;

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

        InvokeRepeating("LaunchMusicNotes", 0f, 0.6f);
    }

    void LaunchMusicNotes()
    {
        bool randomChoice = Random.value < 0.5f;
        
        Instantiate(Target, Track.transform.position + new Vector3(12f, 0f, 0f), Quaternion.identity, Track.transform);
    }
}
