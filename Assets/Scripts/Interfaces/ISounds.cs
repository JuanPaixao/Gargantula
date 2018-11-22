using UnityEngine;

public interface ISounds {
    void StrongAttackEvent(AnimationEvent animationEvent);

    void WeakAttackEvent(AnimationEvent animationEvent);

    void DamagedEvent(AnimationEvent animationEvent);

    void WalkEvent(AnimationEvent animationEvent);

    void JumpEvent(AnimationEvent animationEvent);

    void DeathEvent(AnimationEvent animationEvent);
}
