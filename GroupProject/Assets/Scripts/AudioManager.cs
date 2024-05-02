using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;  // To play background music
    public AudioClip background;  // Background music clip
    [SerializeField] private AudioSource SFXSource;    // To play sound effects
    public AudioClip death;       // Death sound effect
    public AudioClip jump;        // Jump sound effect
    public AudioClip damage;      // Damage sound effect
    public AudioClip heartGain;        // Bonk or hit sound effect
    public AudioClip gravityFlip;  // Change Gravity sound effect

    public AudioClip respawn;  // Change Gravity sound effect

    private AudioClip lastPlayedClip;
    private float lastPlayedTime;
    public float soundCooldown = 0.1f; // Prevent playing the same sound within 100 ms

    private void Start()
    {
        if (background != null) {
            musicSource.clip = background;
            musicSource.Play();
        } else {
            Debug.LogWarning("Background music clip is not assigned in the inspector.");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && (lastPlayedClip != clip || Time.time > lastPlayedTime + soundCooldown))
        {
            SFXSource.PlayOneShot(clip);
            lastPlayedClip = clip;
            lastPlayedTime = Time.time;
        }
        else
        {
            Debug.Log("Sound play request ignored due to cooldown.");
        }
    }
}