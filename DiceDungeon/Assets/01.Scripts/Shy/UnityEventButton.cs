using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent _event;

    public void OnPointerClick(PointerEventData eventData)
    {
        _event?.Invoke();
    }
}
