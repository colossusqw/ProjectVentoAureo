using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SFXManager.Instance.PlaySFX(SFX.MenuButtonHover);
    }
}
