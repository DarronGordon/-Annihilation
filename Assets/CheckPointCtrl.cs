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

    void Start()
    {
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
