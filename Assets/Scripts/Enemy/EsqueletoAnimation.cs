using UnityEngine;

public class EsqueletoAnimation : IBattleAnimation {
    public bool IsAttacking(Animator animator, GameObject weapon)
    {
        if (IsOnAttackingAnimation(animator, weapon))
        {
            weapon.GetComponent<WeaponBehavior>().IsAttacking(true);
            return true;
        }
        weapon.GetComponent<WeaponBehavior>().IsAttacking(false);
        return false;
    }

    public int IsFinishingAttack(Animator animator, GameObject weapon)
    {
        throw new System.NotImplementedException();
    }

    private bool IsOnAttackingAnimation(Animator animator, GameObject weapon)
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("Ataque"))
            return true;
        else if (state.IsTag("Ataque Forte"))
            return true;
        return false;
    }
}
