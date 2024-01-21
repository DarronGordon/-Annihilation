using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogues/New Dialogue")]
public class Dialogue_SO : ScriptableObject
{
    [SerializeField][TextArea(5,5)] private string dialogue;

    public string Dialogue { get => dialogue; set => dialogue = value; }
}
