using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI rankText;
    public Image characterImage;

    public List<Sprite> rankSprites;
    public Button continueButton;

    [SerializeField] private int[] scoreThresholds = { 0, 300, 500, 675, 850, 1000};
    [SerializeField] private string[] rankLetters = { "F", "E", "D", "C", "B", "A" };

    void Start()
    {
        continueButton.gameObject.SetActive(false);

        int points = GameManager.GM.points;
        scoreText.text = $"Score: {points}";

        int rankIndex = CalculateRank(points);

        rankText.text = $"Rank: {rankLetters[rankIndex]}";

        if (rankIndex >= 0 && rankIndex < rankSprites.Count)
            characterImage.sprite = rankSprites[rankIndex];

        StartCoroutine(ShowContinueButton());

        Invoke("PlayTheResultsSFX", 2f);
    }

    private int CalculateRank(int score)
    {
        for (int i = scoreThresholds.Length - 1; i >= 0; i--)
        {
            if (score >= scoreThresholds[i])
                return i;
        }
        return 0;
    }

    private void PlayTheResultsSFX()
    {
        SFXManager.Instance.PlaySFX(SFX.Results);
    }

    IEnumerator ShowContinueButton()
    {
        yield return new WaitForSeconds(5f);
        continueButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(() =>
        {
            continueButton.gameObject.SetActive(false);
            SFXManager.Instance.PlaySFX(SFX.MenuOptionSelected);
            GameManager.GM.ChangeScene("Menu");
        });
    }
}

