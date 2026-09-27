using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footsteps_var0;
    public AudioClip footsteps_var1;
    public AudioClip footsteps_var2;
    public AudioClip footsteps_var3;
    private float audioDelay;

    private AudioClip[] footsteps = new AudioClip[4];

    private Animator playerAnimator;
    private bool isWalking;
    private bool isRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        footsteps[0] = footsteps_var0;
        footsteps[1] = footsteps_var1;
        footsteps[2] = footsteps_var2;
        footsteps[3] = footsteps_var3;

        StartCoroutine(WalkingSFX(footsteps));
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("love-walk");
        isRunning = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("love-run");

        if (isWalking) { audioDelay = 0.45f; }
        else if (isRunning) { audioDelay = 0.2f; }
    }

    IEnumerator WalkingSFX(AudioClip[] audioClips)
    {
        while (true)
        {
            if (isWalking || isRunning)
            {
                AudioClip currentAudioClip = footsteps[UnityEngine.Random.Range(0, 4)];
                float randVolume = UnityEngine.Random.Range(0.3f, 0.4f);
                float randPitch = UnityEngine.Random.Range(0.9f, 1.1f);
                AudioManager.instance.PlaySFX(currentAudioClip, randVolume, randPitch);
            }
            yield return new WaitForSeconds(audioDelay);
        }
    }
}
