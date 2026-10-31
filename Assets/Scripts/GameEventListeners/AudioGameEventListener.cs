using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Attaches to GameObjects as a Listener to invoke UnityEvents that passes audio
/// when the subscribed GameEvent is raised.
/// </summary>
public class AudioGameEventListener : GameEventListener<AudioClip>
{

}
