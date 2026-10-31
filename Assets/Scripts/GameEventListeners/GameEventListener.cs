using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GameEventListener (Subscriber) to subscribe to a GameEvent (Publisher)
/// and invokes a UnityEvent (Response) when the GameEvent is raised
/// 
/// Used for:
/// - Decoupling (GameEventListener does not need to be referenced directly by GameEvent)
/// - Efficiency (Reduces/Prevents constant frame polling)
/// - Scalability (Easier to maintain)
/// </summary>
/// <typeparam name="T">Any Data type can be passed by the event listener.</typeparam>
public class GameEventListener<T> : MonoBehaviour
{
    public GameEvent<T> Event;
    public UnityEvent<T> Response;

    // Subscribes to the specific Game Event when enabled
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    // Unsubscribes to the specific Game Event when disabled
    // Called when object is destroyed, and for cleanup.
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T data)
    {
        Response.Invoke(data);
    }
}
