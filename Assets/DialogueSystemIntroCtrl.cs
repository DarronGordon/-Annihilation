using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystemIntroCtrl : MonoBehaviour
{
#region [[[[[ VARS ]]]]]

    [Header("--NPC#1--")]
    [SerializeField] DialogueList_SO npc1Dialogue; 
    [SerializeField] GameObject npc1SpeachBubble;
    [SerializeField] TextMeshProUGUI npc1SpeachBubbletextMesh;
    [SerializeField] GameObject npc1;

    [Header("--NPC#2--")]
    [SerializeField] DialogueList_SO npc2Dialogue;
    [SerializeField] GameObject npc2SpeachBubble;
    [SerializeField] GameObject npc2;
    [SerializeField] TextMeshProUGUI npc2SpeachBubbletextMesh;
    [SerializeField]GameObject darknessNPC;


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

            default:
                break;
        }
        
    }

    #region [[[[[ LEVEL#1 DIALOGUE SET#1 ]]]]]
    public void SetNpc1Dialogue(int dialogueIndex)
    {
        npc1SpeachBubble.SetActive(true);
        npc1SpeachBubbletextMesh.text = npc1Dialogue.DialogueList[dialogueIndex].Dialogue;

    }
    public void SetNpc2Dialogue(int dialogueIndex)
    {
        npc2SpeachBubble.SetActive(true);
        npc2SpeachBubbletextMesh.text = npc1Dialogue.DialogueList[dialogueIndex].Dialogue;

    }
    public IEnumerator Level1DialogueRoutine() 
    {
        

        SetNpc1Dialogue(0);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc1SpeachBubble.SetActive(false);
        darknessNPC.SetActive(false);
        yield return new WaitForSeconds(.5f);

        SetNpc1Dialogue(1);
        npc1.SetActive(true);
        npc1SpeachBubbletextMesh.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc1SpeachBubble.SetActive(false);
        npc1.SetActive(false);
        npc1SpeachBubbletextMesh.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);

        SetNpc2Dialogue(2);
        npc2.SetActive(true);
        npc2SpeachBubbletextMesh.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc2SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);

        SetNpc2Dialogue(3);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc2SpeachBubble.SetActive(false);
        yield return new WaitForSeconds(.5f);

        SetNpc2Dialogue(4);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc2SpeachBubble.SetActive(false);
        npc2.SetActive(false);
        npc2SpeachBubbletextMesh.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);

        SetNpc1Dialogue(5);
        npc1.SetActive(true);
        npc1SpeachBubbletextMesh.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        npc1SpeachBubble.SetActive(false);
  
        //FADE OUT TO NEXT SCENE
        SceneManagerControler.Instance.FadeOut();

        yield return new WaitForSeconds(1.5f);

        SceneManagerControler.Instance.LoadLevel1Scene();

    }

    #endregion

}
