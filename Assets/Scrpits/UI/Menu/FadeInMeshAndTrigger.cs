using System.Collections;
using UnityEngine;

public class FadeInMeshAndTrigger : MonoBehaviour
{
    public Renderer targetRenderer;
    public float fadeDuration = 1f;
    public GameObject objectToActivate;

    private Material materialInstance;
    private Color baseColor;

    void OnEnable()
    {
        materialInstance = targetRenderer.material;
        baseColor = materialInstance.color;
        baseColor.a = 0f;
        materialInstance.color = baseColor;

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            baseColor.a = alpha;
            materialInstance.color = baseColor;
            yield return null;
        }

        baseColor.a = 1f;
        materialInstance.color = baseColor;

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
