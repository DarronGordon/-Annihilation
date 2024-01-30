using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggeredSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]AudioClip soundToPlay;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(soundToPlay);
    }
}
