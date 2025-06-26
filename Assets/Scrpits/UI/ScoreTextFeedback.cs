using System.Collections;
using UnityEngine;

public class ScoreTextFeedback : MonoBehaviour, IFeedbackReactive
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void ReactToFeedback(float scaleMultiplier, float duration)
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(AnimateScale(scaleMultiplier, duration));
    }

    private IEnumerator AnimateScale(float scaleMultiplier, float duration)
    {
        Vector3 targetScale = originalScale * scaleMultiplier;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        rectTransform.localScale = originalScale;
    }
}

