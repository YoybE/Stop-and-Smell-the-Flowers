using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Game Constants")]
    [SerializeField] private GameConstants _gameConstants;
    [SerializeField] private PlayerAudioConstants _playerAudioConstants;

    [Header("Audio Groups")]
    public AudioMixerGroup SFXMixerGroup;

    void Start()
    {
        PlayBackgroundMusic();
    }

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

    public void PlayBackgroundMusic()
    {
        PlaySFX(_gameConstants.backgroundMusic, 1.0f, 1.0f, _gameConstants.MusicMixerGroup, true);
    }

    public void StopAllAudio()
    {
        AudioSource[] audioSources = UnityEngine.Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
    }

    public void onHit()
    {
        PlaySFX(_gameConstants.hitSFX, 0.3f, 1.0f, SFXMixerGroup);
    }

    public void onPlayFootsteps()
    {
        PlaySFX(_playerAudioConstants.currentAudioClip, _playerAudioConstants.currentVolume, _playerAudioConstants.currentPitch, SFXMixerGroup);
    }
}
