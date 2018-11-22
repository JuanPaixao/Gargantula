using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    void BaseAttack();
    void AlternativeAttack();
    IEnumerator RestoreStamina(int seconds);
    bool IsNextToPlayer(int proximity);
    bool IsAggressive();
    bool IsChasing();
}
