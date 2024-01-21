using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCGirl : MonoBehaviour
{
    int ind =0;

    [SerializeField]DialogueList_SO dialogueList;

    [SerializeField] TextMeshProUGUI speachBubbleTxt;
    [SerializeField] GameObject speachBubble;

    public void DialogueCtrl()
    {
        speachBubble.SetActive(true);
        speachBubbleTxt.text = dialogueList.DialogueList[ind].Dialogue;

        ind++;
    }

    public void DisableSpeachBubble()
    {
        speachBubble.SetActive(false);
    }
}
