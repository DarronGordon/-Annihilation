using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartRattin();
        }
    }

    private void StartRattin()
    {
        Animator animator = GetComponent<Animator>();

        animator.Play("Rat");
    }

    public void Dead()
    {
        Destroy(gameObject); 
    }
}
