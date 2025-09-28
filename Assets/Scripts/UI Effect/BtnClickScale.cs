using UnityEngine;
using UnityEngine.EventSystems;
public class BtnClickScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Range(1, 2)][SerializeField] float maxScaleValue = 1.1f;
    public void OnPointerDown(PointerEventData eventData) => transform.localScale = Vector3.one * maxScaleValue;
    public void OnPointerUp(PointerEventData eventData) => transform.localScale = Vector3.one;
}