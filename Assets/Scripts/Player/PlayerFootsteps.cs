using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerFootsteps : MonoBehaviour
{
    // [Header("Footstep Audio Clips")]
    // public AudioClip footsteps_var0;
    // public AudioClip footsteps_var1;
    // public AudioClip footsteps_var2;
    // public AudioClip footsteps_var3;
    // public AudioMixerGroup audioMixerGroup;
    // private float audioDelay;

    [Header("Player Audio Constants")]
    public PlayerAudioConstants audioConstants;

    [Header("Game Events")]
    [SerializeField] private SimpleGameEvent onPlayFootsteps;

    private AudioClip[] footsteps = new AudioClip[4];

    private Animator playerAnimator;
    private bool isWalking;
    private bool isRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        audioConstants.footsteps[0] = audioConstants.footsteps_var0;
        audioConstants.footsteps[1] = audioConstants.footsteps_var1;
        audioConstants.footsteps[2] = audioConstants.footsteps_var2;
        audioConstants.footsteps[3] = audioConstants.footsteps_var3;

        StartCoroutine(WalkingSFX(footsteps));
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("love-walk");
        isRunning = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("love-run");

        if (isWalking) { audioConstants.currentAudioDelay = audioConstants.walkDelay; }
        else if (isRunning) { audioConstants.currentAudioDelay = audioConstants.runDelay; }
    }

    IEnumerator WalkingSFX(AudioClip[] audioClips)
    {
        while (true)
        {
            if (isWalking || isRunning)
            {
                audioConstants.currentAudioClip = audioConstants.footsteps[UnityEngine.Random.Range(0, 4)];
                audioConstants.currentVolume = UnityEngine.Random.Range(0.3f, 0.4f);
                audioConstants.currentPitch = UnityEngine.Random.Range(0.9f, 1.1f);
                // AudioManager.instance.PlaySFX(currentAudioClip, randVolume, randPitch, audioMixerGroup);
                onPlayFootsteps.Raise();
            }
            yield return new WaitForSeconds(audioConstants.currentAudioDelay);
        }
    }
}
