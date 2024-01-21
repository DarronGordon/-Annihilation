using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue List", menuName = "ScriptableObjects/Dialogues/New Dialogue List")]
public class DialogueList_SO : ScriptableObject
{
    [SerializeField] private string name;

    [SerializeField] private List<Dialogue_SO> dialogueList = new List<Dialogue_SO>();

    public List<Dialogue_SO> DialogueList { get => dialogueList; set => dialogueList = value; }

  
}
