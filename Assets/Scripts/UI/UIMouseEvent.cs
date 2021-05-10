using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnHover;
    public UnityEvent OnExitHover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitHover.Invoke();
    }
}
