using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    //private void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //    audioSource.clip = backgroundMusic;
    //    audioSource.loop = true; // Enable looping
    //    audioSource.Play(); // Start playing the music
    //}

    private void OnEnable()
    {
       if(audioSource == null)
        {
            audioSource=GetComponent<AudioSource>();
        }
       if(!audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // Enable looping
            audioSource.Play(); // Start playing the music
        }
    }
}
