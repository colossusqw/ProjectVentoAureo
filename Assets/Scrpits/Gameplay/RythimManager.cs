using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RythimManager : MonoBehaviour
{
    public TMP_Text textPoints;

    public RythimButton ButtonA;

    public GameObject Target;
    public GameObject Track;

    [SerializeField] public List<rythmUnit> LevelList;

    public int currentIndex = 0;
    public int subIndex = 0;
    public int LevelLength;

    public bool playing = false;
    public float targetvel = 5f;
    public float rythim = 105f;

    public float GreatDist = 0.1f;
    public float GoodDist = 0.3f;
    public float OkDist = 0.6f;
    public float BadDist = 1.2f;

    public int points = 0;

    void Awake()
    {
        LevelLength = LevelList.Count;
        Invoke("StartRound", 6f);
    }

    public void UpdatePointText()
    {
        textPoints.text = "Score: " + points.ToString();
    }

    void StartRound() 
    {
        playing = true;

        InvokeRepeating("LaunchMusicNotes", 0f, 60f/rythim);
    }

    void LaunchMusicNotes()
    {
        if(currentIndex >= LevelLength - 1) playing = false;
        if(!playing) return;

        bool randomChoice = Random.value < 0.5f;

        Instantiate(Target, Track.transform.position + new Vector3(12f, 0f, 0f), Quaternion.identity, Track.transform);

        if(LevelList[currentIndex].reps > subIndex)
        {
            subIndex++;
        }
        else
        {
            subIndex = 0;
            currentIndex++;
        }
    }
}

[System.Serializable] public struct rythmUnit
{
    public int type; // 0 equals none, 1 equals a, 2 equals b, 3 equals both
    public int reps; // how many times it repeats
}