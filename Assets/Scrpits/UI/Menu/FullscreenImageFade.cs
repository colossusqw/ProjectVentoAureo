using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullscreenImageFade : MonoBehaviour
{
    public Image fullscreenImage;
    public float timeOnScreen = 2f;
    public float fadeDuration = 1.5f;
    public float timeToActivateObject = 1f;
    public GameObject objectToActivate;

    private void Start()
    {
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        Color color = fullscreenImage.color;
        color.a = 1f;
        fullscreenImage.color = color;

        yield return new WaitForSeconds(timeOnScreen);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            color.a = alpha;
            fullscreenImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fullscreenImage.color = color;

        yield return new WaitForSeconds(timeToActivateObject);

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}

