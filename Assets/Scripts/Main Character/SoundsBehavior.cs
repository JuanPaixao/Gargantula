using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsBehavior : MonoBehaviour, ISounds{
    public AudioClip strong_attack, strong_attack_weapon;
    public AudioClip weak_attack_1, weak_attack_2, weak_attack_3;
    public AudioClip weak_attack_weapon_1, weak_attack_weapon_2, weak_attack_weapon_3;
    public AudioClip damaged_1, damaged_2;
    public AudioClip walk_r, walk_l;
    public AudioClip jump, jump_end;
    public AudioClip death;

    AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GameObject.Find("Noah").GetComponent<AudioSource>();
    }

    public void StrongAttackEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(strong_attack);
    }

    public void StrongAttackWeaponEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(strong_attack_weapon);
    }

    public void WeakAttackEvent(AnimationEvent animationEvent)
    {
        switch(animationEvent.intParameter)
        {
            case 0:
                _audioSource.PlayOneShot(weak_attack_1);
                break;
            case 1:
                _audioSource.PlayOneShot(weak_attack_2);
                break;
            case 2:
                _audioSource.PlayOneShot(weak_attack_3);
                break;
        }
    }

    public void WeakAttackWeaponEvent(AnimationEvent animationEvent)
    {
        switch (animationEvent.intParameter)
        {
            case 0:
                _audioSource.PlayOneShot(weak_attack_weapon_1);
                break;
            case 1:
                _audioSource.PlayOneShot(weak_attack_weapon_2);
                break;
            case 2:
                _audioSource.PlayOneShot(weak_attack_weapon_3);
                break;
        }
    }

    public void DamagedEvent(AnimationEvent animationEvent)
    {
        switch (animationEvent.intParameter)
        {
            case 0:
                _audioSource.PlayOneShot(damaged_1);
                break;
            case 1:
                _audioSource.PlayOneShot(damaged_2);
                break;
        }
    }

    public void WalkEvent(AnimationEvent animationEvent)
    {
        //if (leg)
        //    audioSource.PlayOneShot(walk_l);
        //else
        //    audioSource.PlayOneShot(walk_r);
    }

    public void JumpEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(jump);
    }

    public void JumpEndEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(jump_end);
    }

    public void DeathEvent(AnimationEvent animationEvent)
    {
        _audioSource.PlayOneShot(death);
    }
}
