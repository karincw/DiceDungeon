using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent _event;

    public void OnPointerClick(PointerEventData eventData)
    {
        _event?.Invoke();
    }
}
