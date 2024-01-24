using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField]DialogueSystemIntroCtrl dsc;
    void Start()
    {
        dsc.StartDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
