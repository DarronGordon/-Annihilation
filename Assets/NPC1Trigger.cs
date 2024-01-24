using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Trigger : MonoBehaviour
{
    [SerializeField]GameObject dialogueObject;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            dialogueObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
                if(other.gameObject.CompareTag("Player"))
        {
            dialogueObject.SetActive(false);
        }
    }
}
