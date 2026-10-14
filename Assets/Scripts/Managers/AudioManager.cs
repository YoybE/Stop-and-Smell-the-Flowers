using System;
using System.Collections;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public void PlaySFX(AudioClip audioClip, float volume, float pitch, AudioMixerGroup audioMixerGroup, Boolean loop = false)
    {
        StartCoroutine(PlaySFXCoroutine(audioClip, volume, pitch, audioMixerGroup, loop));
    }

    IEnumerator PlaySFXCoroutine(AudioClip audioClip, float volume, float pitch, AudioMixerGroup audioMixerGroup, Boolean loop)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.loop = loop;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(audioSource);
    }
}
