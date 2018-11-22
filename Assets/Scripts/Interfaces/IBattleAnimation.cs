using UnityEngine;

interface IBattleAnimation {
    bool IsAttacking(Animator animator, GameObject weapon);

    int IsFinishingAttack(Animator animator, GameObject weapon);
}
