using UnityEngine;

public class BattleAnimation : IBattleAnimation
{
    public bool IsAttacking(Animator animator, GameObject weapon)
    {
        if (IsOnAttackingAnimation(animator, weapon))
        {
            animator.SetBool("Attacking", true);
            (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).IsAttacking(true);
            return true;
        }
        else if (IsOnPrepareAttackAnimation(animator))
        {
            animator.SetBool("Attacking", true);
            (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).IsAttacking(false);
            return true;
        }
        animator.SetBool("Attacking", false);
        (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).IsAttacking(false);
        return false;
    }

    public int IsFinishingAttack(Animator animator, GameObject weapon)
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("Finishing Attack 1"))
        {
            (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).CanAttackAgain(true);
            return 1;
        }
        else if (state.IsTag("Finishing Attack 2"))
        {
            (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).CanAttackAgain(true);
            return 2;
        }
        (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).CanAttackAgain(false);
        return 0;
    }

    private bool IsOnPrepareAttackAnimation(Animator animator)
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("Prepare Attack"))
            return true;
        return false;
    }

    private bool IsOnAttackingAnimation(Animator animator, GameObject weapon)
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("Attack") || state.IsTag("Attack 2") || state.IsTag("Attack 3"))
            return true;
        else if (state.IsTag("Attack Strong"))
        {
            (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).IsStrongAttack = true;
            return true;
        }
        (weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).IsStrongAttack = false;
        return false;
    }
}
