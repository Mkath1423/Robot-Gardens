using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameEventListener : MonoBehaviour
{

    public GameEvent Event;
    public UnityEvent Responce;

    private void OnEnable() { Event.RegisterListener(this); }
    private void OnDisable() { Event.UnregisterListener(this); }

    public void OnEventRaised() { Responce.Invoke(); }
    
}
