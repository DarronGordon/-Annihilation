using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundCtrl : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]AudioClip takeDmgSound;
    [SerializeField]AudioClip attackSound;
    [SerializeField]AudioClip jumpSound;
    [SerializeField]AudioClip dieSound;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTakeDmg()
    {
        audioSource.PlayOneShot(takeDmgSound);
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void Die()
    {
        audioSource.PlayOneShot(dieSound);
    }
}
