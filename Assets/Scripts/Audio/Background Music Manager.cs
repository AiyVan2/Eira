using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Enable looping
        audioSource.Play(); // Start playing the music
    }

    private void OnEnable()
    {
        // This is optional, as the music will already be set in Start.
        audioSource.clip = backgroundMusic;
    }
}
