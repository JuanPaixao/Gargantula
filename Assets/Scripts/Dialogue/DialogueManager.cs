using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Queue<string> _sentences;

    private TextMeshProUGUI _textMeshPro;
    private Image _textPanel;
    private IDialogue _callback;
    private bool _startedDialogue;

    void Start() {   }

    void Update()
    {
        if (!IsTalking())
            return;
        bool attack = Input.GetButtonDown("Fire1");
        if (attack && _startedDialogue)
            NextSentence();
    }

    internal void StartDialogue(Dialogue dialogue, IDialogue callback)
    {
        GameObject canvas = GameObject.Find("Text Panel");
        _textPanel = canvas.transform.GetComponent<Image>();
        _textPanel.color = new Color32(0, 0, 0, 255);
        _sentences = new Queue<string>();
        foreach (string sentence in dialogue.sentences)
            _sentences.Enqueue(sentence);
        _textMeshPro = TextMeshProUGUI.FindObjectOfType<TextMeshProUGUI>();
        _callback = callback;
        NextSentence();
        _startedDialogue = true;
    }

    private void NextSentence()
    {

        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        _textMeshPro.text = _sentences.Dequeue();
    }

    private void EndDialogue()
    {
        _startedDialogue = false;
        _sentences = null;
        _textMeshPro.text = null;
        _textPanel.color = new Color32(0, 0, 0, 0);
        _callback.FinishedDialogue();
    }

    public bool IsTalking()
    {
        if (_sentences == null)
            return false;
        return true;
    }
}
