using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public enum JudgementType
{
    Schifoso, Bene, Grande, Eccellente
}


public class PerformanceFeedbackController : MonoBehaviour
{
    public Renderer characterRenderer;
    public Transform characterTransform;
    public List<Texture> characterTextures;
    public Image playerScore;
    public LightControler FeedbackLight;

    public Image feedbackImageUI;
    public RectTransform feedbackTransform;
    public List<Sprite> feedbackSprites;
    public float feedbackFadeDuration = 1.0f;

    public List<float> scaleMultipliers;
    private Vector3 originalCharacterScale;
    private Vector3 originalFeedbackScale;

    private Coroutine feedbackCoroutine;

    private void Start()
    {
        if (characterTransform != null)
            originalCharacterScale = characterTransform.localScale;

        if (feedbackTransform != null)
            originalFeedbackScale = feedbackTransform.localScale;
    }

    public void ShowFeedback(JudgementType type)
    {
        int index = (int)type;

        if (index >= 0 && index < characterTextures.Count)
        {
            FeedbackLight.StartFeedback(index);
            characterRenderer.material.mainTexture = characterTextures[index];
        }

        if (index >= 0 && index < feedbackSprites.Count)
        {
            feedbackImageUI.sprite = feedbackSprites[index];
            feedbackImageUI.color = new Color(1, 1, 1, 1);

            float scale = (index < scaleMultipliers.Count) ? scaleMultipliers[index] : 1f;

            if (characterTransform != null) characterTransform.localScale = originalCharacterScale * scale;

            if (feedbackTransform != null) feedbackTransform.localScale = originalFeedbackScale * scale;

            if (feedbackCoroutine != null) StopCoroutine(feedbackCoroutine);

            feedbackCoroutine = StartCoroutine(FadeOutFeedback());

            var allReactions = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (var obj in allReactions)
            {
                if (obj is IFeedbackReactive reactive)
                {
                    reactive.ReactToFeedback(scale * 1.15f, feedbackFadeDuration * 0.5f);
                }
            }

            playerScore.GetComponent<ScoreTextFeedback>().ReactToFeedback(scale, feedbackFadeDuration * 0.5f);
        }
    }

    private IEnumerator FadeOutFeedback()
    {
        float timer = 0f;
        Color originalColor = feedbackImageUI.color;

        while (timer < feedbackFadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / feedbackFadeDuration;

            float alpha = Mathf.Lerp(1f, 0f, t);
            feedbackImageUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (characterTransform != null)
            {
                characterTransform.localScale = Vector3.Lerp(characterTransform.localScale, originalCharacterScale, t);
            }

            if (feedbackTransform != null)
            {
                feedbackTransform.localScale = Vector3.Lerp(feedbackTransform.localScale, originalFeedbackScale, t);
            }

            yield return null;
        }

        if (characterTransform != null) characterTransform.localScale = originalCharacterScale;

        if (feedbackTransform != null) feedbackTransform.localScale = originalFeedbackScale;

        feedbackImageUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
