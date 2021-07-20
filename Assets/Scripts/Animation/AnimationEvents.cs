using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public List<Events> events = new List<Events>();
    public void CallEvent(int index)
    {
        if (events.Count > index)
            events[index].m_event.Invoke();
    }
}
[System.Serializable]
public class Events
{
    public UnityEvent m_event;
}
