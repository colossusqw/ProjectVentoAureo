using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderBoxSystem : MonoBehaviour
{
    public CanvasGroup orderBoxGroup;
    public Image orderBoxArt;
    public Image[] orderImages = {null, null, null};
    public Sprite[] orderImagesSprites = { null, null, null };

    public float fadeDuration = 0.2f;
    public float scaleFactor = 1.125f;
    public float scaleDuration = 0.05f;

    private Vector3 defaultScale;

    private System.Action onOrderEndCallback;

    private void Start()
    {
        defaultScale = orderBoxArt.rectTransform.localScale;
        orderBoxGroup.alpha = 0f;
        orderBoxGroup.gameObject.SetActive(false);
    }

    public void StartOrderBox(System.Action onOrderEnd = null)
    {
        orderBoxGroup.gameObject.SetActive(true);

        onOrderEndCallback = onOrderEnd;

        for (int i = 0; i < orderImages.Length; i++)
        {
            if (i < orderImagesSprites.Length && orderImagesSprites[i] != null)
            {
                orderImages[i].sprite = orderImagesSprites[i];
                orderImages[i].gameObject.SetActive(true);
            }
            else
            {
                orderImages[i].sprite = null;
                orderImages[i].gameObject.SetActive(false);
            }
        }

        StartCoroutine(FadeIn());
        StartCoroutine(ScaleBox());
    }

    public void EndOrderBox()
    {
        StartCoroutine(FadeOutAndClose());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        orderBoxGroup.alpha = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            orderBoxGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        orderBoxGroup.alpha = 1f;
    }
    
    private IEnumerator FadeOutAndClose()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            orderBoxGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        orderBoxGroup.alpha = 0f;
        orderBoxGroup.gameObject.SetActive(false);

        foreach (var img in orderImages)
        {
            img.sprite = null;
            img.gameObject.SetActive(false);
        }

        onOrderEndCallback?.Invoke();
    }

    private IEnumerator ScaleBox()
    {
        float t = 0f;
        Vector3 targetScale = defaultScale * scaleFactor;

        while (t < scaleDuration)
        {
            t += Time.deltaTime;
            orderBoxArt.rectTransform.localScale = Vector3.Lerp(defaultScale, targetScale, t / scaleDuration);
            yield return null;
        }

        t = 0f;
        while (t < scaleDuration)
        {
            t += Time.deltaTime;
            orderBoxArt.rectTransform.localScale = Vector3.Lerp(targetScale, defaultScale, t / scaleDuration);
            yield return null;
        }

        orderBoxArt.rectTransform.localScale = defaultScale;
    }
}
