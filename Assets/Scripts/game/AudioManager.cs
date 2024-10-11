using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource footstepsSource;

    public AudioClip background;
    public AudioClip attack;
    public AudioClip heavyAttack;
    public AudioClip block;
    public AudioClip damage;
    public AudioClip death;
    public AudioClip changeProta;
    public AudioClip footsteps;
    public AudioClip footstepsRun;

    [SerializeField] Player player;
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void playAudio(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);    
    }
    public void playFootstep(bool isSprinting)
    {
        AudioClip clipToPlay = isSprinting ? footstepsRun : footsteps; 

      
        if (!footstepsSource.isPlaying || footstepsSource.clip != clipToPlay)
        {
            footstepsSource.clip = clipToPlay; 
            footstepsSource.Play();
        }
    }

    public void stopFootstep()
    {
        footstepsSource.Stop();
    }
    public AudioClip audioFootstep
    {
        get { return footstepsSource.clip; }
    }
}
