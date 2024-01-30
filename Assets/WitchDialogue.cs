using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WitchDialogue : MonoBehaviour
{
      [Header("--NPC#1--")]
    [SerializeField] DialogueList_SO npcDialogue; 
    [SerializeField] GameObject npcSpeachBubble;
    [SerializeField] TextMeshProUGUI npcSpeachBubbletextMesh;
    public void StartDialogue()
    {
         StartCoroutine(Level1DialogueRoutine());
    }
    public void SetNpcDialogue(int dialogueIndex)
    {
        npcSpeachBubble.SetActive(true);
        npcSpeachBubbletextMesh.text = npcDialogue.DialogueList[dialogueIndex].Dialogue;

    }
public IEnumerator Level1DialogueRoutine() 
    {
        

        SetNpcDialogue(0);
        yield return new WaitForSeconds(5f);
        npcSpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.7f);
        SetNpcDialogue(1);
        yield return new WaitForSeconds(3f);
        npcSpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.7f);



    }

}
