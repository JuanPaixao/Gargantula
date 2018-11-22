using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{

    int _damage;
    string _holderEnemyTag;
    StatBehaviour _holder;
    Animator _animator;

    public bool IsStrongAttack { get; set; }

    public void StartBehaviour()
    {
        _holder.updateBaseDamage();
        gameObject.AddComponent(typeof(BoxCollider));
        BoxCollider c = gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
        c.center = new Vector3(0, 0.5f, 0);
        c.size = new Vector3(1, 2, 0.5f);
    }

    public void SetHolder(StatBehaviour statBehaviour)
    {
        _holder = statBehaviour;
    }

    public void SetHolderEnemyTag(string tag)
    {
        _holderEnemyTag = tag;
    }

    public void SetHolderAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void IsDefending(bool isDefending)
    {
        Collider c = gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
        c.isTrigger = isDefending;
        _holder.setDefend(isDefending);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _holderEnemyTag)
        {
            if (_holder.stamina >= 5)
                _animator.SetTrigger("Defending Hit");
            else if (_holder.stamina < 5)
                _animator.SetTrigger("Defending Hit Without Stamina");
        }
    }

}
