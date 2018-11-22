using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    int _damage;
    string _holderEnemyTag;
    string _holderShieldTag = "Shield";
    StatBehaviour _holder;
    List<StatBehaviour> _enemysHitted;

    public bool IsStrongAttack { get; set; }

    public void StartBehaviour()
    {
        _enemysHitted = new List<StatBehaviour>();
        _holder.updateBaseDamage();
        IsStrongAttack = false;
        InstantiateComponents();
    }

    public void CanAttackAgain(bool canAttack)
    {
        Light l = gameObject.GetComponent(typeof(Light)) as Light;
        if (canAttack)
        {
            l.enabled = true;
            return;
        }
        l.enabled = false;
    }

    public void setHolder(StatBehaviour statBehaviour)
    {
        _holder = statBehaviour;
    }

    public void setHolderEnemyTag(string tag)
    {
        _holderEnemyTag = tag;
    }

    public void setAttackDamage(int damage)
    {
        _damage = damage;
    }

    public void IsAttacking(bool isAttacking)
    {
        Collider c = gameObject.GetComponent(typeof(CapsuleCollider)) as CapsuleCollider;
        if (isAttacking)
        {
            c.enabled = true;
            c.isTrigger = true;
        }
        else
        {
            c.enabled = false;
            c.isTrigger = false;
            _enemysHitted.Clear();
        }
    }

    private void Attacking(StatBehaviour enemy, bool enemyDefending)
    {
        if (_enemysHitted.Contains(enemy))
            return;
        if (IsStrongAttack)
            _holder.ApplyDamage2(enemy);
        else
            _holder.ApplyDamage1(enemy);
        _enemysHitted.Add(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        StatBehaviour enemy = null;
        bool enemyDefending = false;
        if (other.tag == _holderShieldTag)
        {
            enemy = other.gameObject.transform.root.GetComponent(typeof(StatBehaviour)) as StatBehaviour;
            enemyDefending = true;
        }

        if (other.tag == _holderEnemyTag || enemy != null)
        {
            if (enemy == null)
                enemy = other.gameObject.GetComponent(typeof(StatBehaviour)) as StatBehaviour;
            Attacking(enemy, enemyDefending);
        }
    }

    private void InstantiateComponents()
    {
        gameObject.AddComponent(typeof(Light));
        Light l = gameObject.GetComponent(typeof(Light)) as Light;
        l.intensity = 3;
        l.range = 1;
        l.enabled = false;
    }
}
