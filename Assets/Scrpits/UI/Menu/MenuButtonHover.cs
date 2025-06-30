using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject leftPointer;
    public GameObject rightPointer;

    private void Start()
    {
        if (leftPointer != null) leftPointer.SetActive(false);
        if (rightPointer != null) rightPointer.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SFXManager.Instance.PlaySFX(SFX.MenuButtonHover);

        if (leftPointer != null) leftPointer.SetActive(true);
        if (rightPointer != null) rightPointer.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideImages();
    }

    private void OnDisable()
    {
        HideImages();
    }

    private void HideImages()
    {
        if (leftPointer != null) leftPointer.SetActive(false);
        if (rightPointer != null) rightPointer.SetActive(false);
    }
}

