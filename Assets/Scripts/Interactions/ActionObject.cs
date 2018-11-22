using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ActionObject : MonoBehaviour {

    public Animator anim;
    public AudioSource audioSource;
    public AudioClip action, deaction;
    bool isactive;

    private void Start()
    {
        isactive = false;
        audioSource = this.GetComponent<AudioSource>();
    }

    public virtual void Action()
    {
        isactive = true;
        //anim.Play("action");
        anim.SetBool("act", true);
    }

    public virtual void Deaction()
    {
        isactive = false;
        //anim.Play("deaction");
        anim.SetBool("act", false);
    }

    void ActionEvent(AnimationEvent animationEvent)
    {
        audioSource.PlayOneShot(action);
    }

    void DeActionEvent(AnimationEvent animationEvent)
    {
        audioSource.PlayOneShot(deaction);
    }
}
