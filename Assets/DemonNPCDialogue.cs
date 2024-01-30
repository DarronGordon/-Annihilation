using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonNPCDialogue : MonoBehaviour
{
    [SerializeField]GameObject txt;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            txt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
                if(other.gameObject.CompareTag("Player"))
        {
            txt.SetActive(false);
        }
    }
}
