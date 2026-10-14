using UnityEngine;
using UnityEngine.Audio;
using TMPro;

[CreateAssetMenu(fileName = "GameConstants", menuName = "Scriptable Objects/GameConstants")]
public class GameConstants : ScriptableObject
{
    [Header("Player Variables")]
    public float walkSpeed = 13.0f;
    public float maxWalkSpeed = 13.5f;
    public float runSpeed = 16.0f;
    public float maxRunSpeed = 16.5f;
    public float jumpSpeed = 11.0f;

    [Header("Enemy Variables")]
    public float enemyPatrolSpeed = 2;
    public float enemyMaxOffset = 1.0f;
    public float enemyPatrolTime = 2.0f;
    public int enemyInitialHp = 2;

    [Header("Audio")]
    public AudioClip backgroundMusic;
    public AudioMixerGroup MusicMixerGroup;
}
