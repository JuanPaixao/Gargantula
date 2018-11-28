using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{

    private Queue<string> _sentences;
    public Text dialogueText, nameText;
    public GameObject[] dialogues, panel, characters;
    private int _dialogNumber, _count;
    public Sprite[] noahSprites;

    void Awake()
    {
        _sentences = new Queue<string>();
        _dialogNumber = 0;
    }
    void Start()
    {
        dialogues[_dialogNumber].SetActive(true);
    }
    void Update()
    {
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("begin");
        nameText.text = dialogue.characterName;
        ActivateDialogueSprite(dialogue.characterName, dialogue.humor);
        _sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        Debug.Log(_sentences.Count);
        if (_sentences.Count == 0)
        {
            _dialogNumber += 1;
            if (_dialogNumber < dialogues.Length)
            {
                dialogues[_dialogNumber].SetActive(true);
            }
            return;
        }
        else
        {
            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(DialoguesCoroutine(sentence));
        }
    }
    public void ActivateDialogueSprite(string name, string humor)
    {
        if (name == "Noah")
        {
            switch (humor)
            {
                case "assustado":
                    characters[0].GetComponent<Image>().sprite = noahSprites[0];
                    characters[0].SetActive(true);
                    break;
                case "pensativo":
                    characters[0].GetComponent<Image>().sprite = noahSprites[1];
                    characters[0].SetActive(true);
                    break;
                case "destemido":
                    characters[0].GetComponent<Image>().sprite = noahSprites[2];
                    characters[0].SetActive(true);
                    break;
            }
        }
        else
        {
            characters[1].SetActive(true);
        }
    }
    private IEnumerator DialoguesCoroutine(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
