using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Trigger : MonoBehaviour
{

[SerializeField]WitchDialogue witchDialogue;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            witchDialogue.StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
                if(other.gameObject.CompareTag("Player"))
        {
        }
    }
}
