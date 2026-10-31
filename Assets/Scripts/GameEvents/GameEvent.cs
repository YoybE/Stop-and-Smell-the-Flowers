using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// GameEvent (Publisher) to notify all subscribed GameEventListener (Subscriber)
/// whenever a specific event has been raised.
/// 
/// Used for:
/// - Decoupling (GameEvent does not need any direct references)
/// - Scalability (Easier to maintain)
/// </summary>
/// <typeparam name="T">Any Data type can be passed by the event. (Accounts for varying data types and data amounts)</typeparam>
public class GameEvent<T> : ScriptableObject
{
    // Attaches a list of event listeners to be subscribed to this GameEvent
    private readonly List<GameEventListener<T>> eventListeners =
        new List<GameEventListener<T>>();

    // Notifies the Event Listener when Event is triggered so that the listener may execute related code
    public void Raise(T data)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(data);
    }

    // Adds event listener to the list if it is not already added
    public void RegisterListener(GameEventListener<T> listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    // Removes event listener from the list if it exists in the list
    public void UnregisterListener(GameEventListener<T> listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
