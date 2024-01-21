using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDialogueCtrl : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] DialogueSystemControl dsc;

    [SerializeField] Transform[] setpoints;
    [SerializeField] float moveSpeedAnim;
    [SerializeField] bool isInAnimState;
    [SerializeField] int moveInd;
    bool dialogueStarted = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();


    }


    private void FixedUpdate()
    {
        if (isInAnimState)
        {
         
            if(transform.position.x < setpoints[moveInd].position.x)
            {
                rb.velocity = new Vector2(moveSpeedAnim *Time.deltaTime,0f);

                anim.Play("Run");
                dialogueStarted = false;
            }
            else if (transform.position.x >= setpoints[moveInd].position.x)
            {
                if(!dialogueStarted) 
                {
                    dsc.StartDialogue(moveInd);
                    dialogueStarted = true;
                }

                rb.velocity = Vector2.zero;

                anim.Play("Idle");
            }
        }
    }

    public void SetMoveInd(int v)
    {
        moveInd = v;
    }
}
