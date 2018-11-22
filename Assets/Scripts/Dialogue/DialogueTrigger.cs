using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IDialogue
{

    public Dialogue _dialogue;
    public AudioClip[] _sounds;
    private DialogueManager _dialogueManager;
    private AudioSource _audioSource;
    private Animator _animator;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void TriggerDialogue()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _dialogueManager.StartDialogue(_dialogue, this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_dialogueManager == null)
            {
                float pressUp = Input.GetAxis("Vertical");
                if (pressUp > 0)
                {
                    TriggerDialogue();
                    if (_animator)
                        _animator.SetTrigger("Talk");
                }
            }
        }
    }

    public void FinishedDialogue()
    {
        _dialogueManager = null;
    }
}
