using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip PotionSound;
    public AudioClip DamageSound;
    public AudioClip DeathSound;
    public AudioClip LevelExitSound;


    private AudioSource _damageAudioSource;
    private AudioSource _potionAudioSource;
    private AudioSource _deathAudioSource;
    private AudioSource _levelExitSource; 

    private void Start()
    {
        _potionAudioSource = gameObject.AddComponent<AudioSource>();
        _potionAudioSource.playOnAwake = false;
        _potionAudioSource.loop = false;
        _potionAudioSource.clip = PotionSound;

        _damageAudioSource = gameObject.AddComponent<AudioSource>();
        _damageAudioSource.playOnAwake = false;
        _damageAudioSource.loop = false;
        _damageAudioSource.clip = DamageSound;

        _deathAudioSource = gameObject.AddComponent<AudioSource>();
        _deathAudioSource.loop = false;
        _deathAudioSource.playOnAwake = false;
        _deathAudioSource.clip = DeathSound;

        _levelExitSource = gameObject.AddComponent<AudioSource>();
        _levelExitSource.loop = false;
        _levelExitSource.playOnAwake = false;
        _levelExitSource.clip = DeathSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPotionPickup()
    {
        _potionAudioSource.Play();
    }

    public void PlayDamageSound()
    {
        _damageAudioSource.Play();
    }

    public void PlayDeathSound()
    {
        _deathAudioSource.Play();
    }

    public void PlayLevelExitSound()
    {
        _levelExitSource.Play();
    }

}
