 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class DialogueSystemControl : MonoBehaviour
{

    #region [[[[[ VARS ]]]]]

    [SerializeField]SceneManagerControlerLevel1 smc;

    [Header("--NPC#1--")]
    [SerializeField] DialogueList_SO npcDialogue; 
    [SerializeField] GameObject npcSpeachBubble;
    [SerializeField] TextMeshProUGUI npcSpeachBubbletextMesh;

    [Header("--NPC#2--")]
    [SerializeField] DialogueList_SO npc2Dialogue;
    [SerializeField] GameObject npc1SpeachBubble;
    [SerializeField] TextMeshProUGUI npc1SpeachBubbletextMesh;

    [Header("--NPC#3--")]
    [SerializeField] DialogueList_SO npc3Dialogue;
    [SerializeField] GameObject npc2SpeachBubble;
    [SerializeField] TextMeshProUGUI npc2SpeachBubbletextMesh;

    [Header("--NPC#4--")]
    [SerializeField] DialogueList_SO npc4Dialogue;
    [SerializeField] GameObject npc3SpeachBubble;
    [SerializeField] TextMeshProUGUI npc3SpeachBubbletextMesh;

    [Header("--PLAYER--")]
    [SerializeField] PlayerDialogueCtrl player;
    [SerializeField] DialogueList_SO PlayerDialogue;
    [SerializeField] GameObject playerSpeachBubble;
    [SerializeField] TextMeshProUGUI playerSpeachBubbletextMesh;


    #endregion

    private void Start()
    {
    }
    public void StartDialogue(int ind)
    {
        switch (ind)
        {
            case 0:
                StartCoroutine(Level1DialogueRoutine());
                break;
            case 1:
                StartCoroutine(Level2DialogueRoutine());
                break;
            case 2:
                StartCoroutine(Level3DialogueRoutine());
                break;
            case 3:
                StartCoroutine(Level4DialogueRoutine());
                break;
            default:
                break;
        }
        
    }

    #region [[[[[ LEVEL#1 DIALOGUE SET#1 ]]]]]
    public void SetNpcDialogue(int dialogueIndex)
    {
        npcSpeachBubble.SetActive(true);
        npcSpeachBubbletextMesh.text = npcDialogue.DialogueList[dialogueIndex].Dialogue;

    }
    public void SetPlayerDialogue(int dialogueIndex)
    {
        playerSpeachBubble.SetActive(true);
        playerSpeachBubbletextMesh.text = PlayerDialogue.DialogueList[dialogueIndex].Dialogue;

    }
    public IEnumerator Level1DialogueRoutine() 
    {
        

        SetNpcDialogue(0);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npcSpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);
        SetNpcDialogue(1);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npcSpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);
        SetNpcDialogue(2);
        smc.FlickerIcon();
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npcSpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);

        SetPlayerDialogue(0);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        playerSpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);


        player.SetMoveInd(1);

    }

    #endregion

    #region [[[[[ LEVEL#1 DIALOGUE SET#2 ]]]]]

    public void SetNpc1Dialogue(int dialogueIndex)
    {
        npc1SpeachBubble.SetActive(true);
        npc1SpeachBubbletextMesh.text = npc2Dialogue.DialogueList[dialogueIndex].Dialogue;

    }
    public IEnumerator Level2DialogueRoutine()
    {
        SetNpc1Dialogue(0);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc1SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);

        player.SetMoveInd(2);
    }
    #endregion

    #region [[[[[ LEVEL#1 DIALOGUE SET#3 ]]]]]

    public void SetNpc2Dialogue(int dialogueIndex)
    {
        npc2SpeachBubble.SetActive(true);
        npc2SpeachBubbletextMesh.text = npc3Dialogue.DialogueList[dialogueIndex].Dialogue;
    }

    public IEnumerator Level3DialogueRoutine()
    {
        SetNpc2Dialogue(0);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc2SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);
        SetNpc2Dialogue(1);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc2SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);

        player.SetMoveInd(3);
    }
    #endregion

    #region [[[[[ LEVEL#1 DIALOGUE SET#4 ]]]]]

    public void SetNpc3Dialogue(int dialogueIndex)
    {
        npc3SpeachBubble.SetActive(true);
        npc3SpeachBubbletextMesh.text = npc4Dialogue.DialogueList[dialogueIndex].Dialogue;

    }
    public IEnumerator Level4DialogueRoutine()
    {
        SetNpc3Dialogue(0);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc3SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f); 
        SetNpc3Dialogue(1);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc3SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);

         player.SetMoveInd(4);
    }
    #endregion

}
