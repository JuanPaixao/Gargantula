using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponSounds{
    void WeakAttack(AudioSource audioSource, int stage);

    void StrongAttack(AudioSource audioSource, int stage);
}
