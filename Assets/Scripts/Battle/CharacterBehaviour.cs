using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public abstract class CharacterBehaviour : StatBehaviour
{
    public GameObject _weapon, _shield;
    public GameObject _hand, _foreArm;

    protected Animator _animator;
    protected CharacterController _controller;
    protected AudioSource _audio;

    protected float _zAxis = 0;
    protected float _speed = 6f;
    protected float _strongAttackSpeed = 0;
    protected float _jumpSpeed = 10.0F;
    protected float _gravity = 20.0F;

    // Variáveis de controle
    protected bool _grounded = true;
    protected bool _falling = false;
    protected bool _attacking = false;
    protected bool _climbing = false;

    // Use this for initialization
    protected void InitializeCharacter()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();

        _zAxis = _controller.transform.position.z;

        StartStatBehavior();
    }

    protected void LockZAxis()
    {
        Vector3 pos = transform.position;
        pos.z = _zAxis;
        transform.position = pos;
    }

    protected void WeaponInstantiate(GameObject weapon)
    {
        _weapon = Instantiate(weapon, _hand.transform.position, _hand.transform.rotation, _hand.transform.parent);
        _weapon.tag = "Weapon";
        (_weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).setHolder(this);
        (_weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).setHolderEnemyTag("Enemy");
        (_weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).StartBehaviour();
    }

    protected void WeaponInstantiate(GameObject weapon, string enemyTag)
    {
        _weapon = Instantiate(weapon, _hand.transform.position, _hand.transform.rotation, _hand.transform.parent);
        _weapon.tag = "Weapon";
        (_weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).setHolder(this);
        (_weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).setHolderEnemyTag(enemyTag);
        (_weapon.GetComponent(typeof(WeaponBehavior)) as WeaponBehavior).StartBehaviour();
    }

    protected void ShieldInstantiate(GameObject shield, string enemyTag, Vector3 position, Vector3 rotation)
    {
        _shield = Instantiate(shield, _foreArm.transform.position, _foreArm.transform.rotation, _foreArm.transform.parent);
        _shield.AddComponent(typeof(ShieldBehavior));
        _shield.transform.localPosition = position;
        _shield.transform.localEulerAngles = rotation;
        (_shield.GetComponent(typeof(ShieldBehavior)) as ShieldBehavior).SetHolder(this);
        (_shield.GetComponent(typeof(ShieldBehavior)) as ShieldBehavior).SetHolderEnemyTag(enemyTag);
        (_shield.GetComponent(typeof(ShieldBehavior)) as ShieldBehavior).SetHolderAnimator(_animator);
        (_shield.GetComponent(typeof(ShieldBehavior)) as ShieldBehavior).StartBehaviour();
    }

    protected bool IsNextToFloor(float distance)
    {
        bool grounded = Physics.Raycast(_controller.transform.position, Vector3.down, distance);
        return grounded;
    }
}
