using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Game Event", menuName ="ScriptableObjects/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(){
        for(int i=listeners.Count -1; i>=0; i--){
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener eventListener){
        listeners.Add(eventListener);
    }

    public void DeregisterListener(GameEventListener eventListener){
        listeners.Remove(eventListener);
    }
}
