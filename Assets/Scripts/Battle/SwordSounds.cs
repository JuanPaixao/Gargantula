using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahEvents : MonoBehaviour
{
    public List<AudioClip> weakAttack;
    public List<AudioClip> strongAttack;

    AudioSource _audioSource = GameObject.Find("Noah").GetComponent<AudioSource>();

    public void NoahStrongAttackEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(strongAttack[animationEvent.intParameter]);
    }

    public void NoahWeakAttackEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(weakAttack[animationEvent.intParameter]);
    }
}
