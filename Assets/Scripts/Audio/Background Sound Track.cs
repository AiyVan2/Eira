using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundTrack : MonoBehaviour
{
    private AudioSource audioSource;

    //Background Music 
    public AudioClip backroundsoundTrack;

    //private void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //    audioSource.clip = backroundsoundTrack;
    //audioSource.loop = true; // Enable looping
    //    audioSource.Play(); // Start playing the music
    //}

    private void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.clip = backroundsoundTrack;
        audioSource.loop = true; // Enable looping

        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // Start playing the music
        }
    }
}
