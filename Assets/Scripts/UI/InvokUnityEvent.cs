using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokUnityEvent : MonoBehaviour
{
    public UnityEvent Event;

    public void InvokEvent()
    {
        Event.Invoke();
    }
}
