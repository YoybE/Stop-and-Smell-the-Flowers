using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        // Ensures that there is only one instance of this Singleton Class
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(AudioClip audioClip, float volume, float pitch, AudioMixerGroup audioMixerGroup)
    {
        StartCoroutine(PlaySFXCoroutine(audioClip, volume, pitch, audioMixerGroup));
    }

    IEnumerator PlaySFXCoroutine(AudioClip audioClip, float volume, float pitch, AudioMixerGroup audioMixerGroup)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(audioSource);
    }
}
