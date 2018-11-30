using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    void Start()
    {
        dialogueManager.StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
