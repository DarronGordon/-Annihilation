using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointCtrl : MonoBehaviour
{
    public bool isCheckPointActive = false;
    public int id;
    Animator anim;

    [SerializeField]AudioClip checkPointFoundSound;
    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("Player"))
        {
            if(isCheckPointActive)
            {
                SetLastCheckPointFound(id, transform.position);
            }
            else
            {
                audioS.PlayOneShot(checkPointFoundSound);
                anim.SetBool("Triggered", true);
                isCheckPointActive = true;
                SetLastCheckPointFound(id, transform.position);
            }


        }
    }

    private void SetLastCheckPointFound(int checkPointID, Vector3 position)
    {
        EventHandlerManager.CallOnPlayerCheckPointActivate(checkPointID, position);
    }
}
