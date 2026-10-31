using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "PlayerAudioConstants", menuName = "Scriptable Objects/PlayerAudioConstants")]
public class PlayerAudioConstants : ScriptableObject
{
    [Header("Forest Footsteps")]
    public AudioClip footsteps_var0;
    public AudioClip footsteps_var1;
    public AudioClip footsteps_var2;
    public AudioClip footsteps_var3;

    [Header("Audio Attributes")]
    public float walkDelay = 0.45f;
    public float runDelay = 0.2f;

    [Header("Current Attributes")]
    public AudioClip[] footsteps = new AudioClip[4];
    public AudioClip currentAudioClip;
    public float currentAudioDelay;
    public float currentPitch;
    public float currentVolume;
}
