using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : CharacterBehaviour
{
    BattleAnimation battleAnimation;

    // Não consigo encontrar o hash do climbing state, então estou utilizando hard coded por enquanto
    static int climbingState = -2086059735;

    Vector3 moveDirection = Vector3.zero;

    Transform _checkpoint;
    public Transform bowHolder;

    public GameObject _startWeapon, _startShield;
    public GameObject arrowModel, arrow;

    public bool _isAttackPressed { get; private set; }
    public int _isFinishingAttack { get; private set; }
    public bool _isStrongAttackPressed { get; private set; }
    public bool _isDefenseHold { get; private set; }

    string enemyTag = "Enemy";
    public float _jumpGravity = 2f;
    public float _jumpForce = 10f;

    void Start()
    {
        InitializeCharacter();
        StartStatBehavior();
        battleAnimation = new BattleAnimation();

        // Só para propositos de teste, tá gigante mesmo e não deveriam estar aqui
        if (_startWeapon && _startShield)
        {
            WeaponInstantiate(_startWeapon);
            ShieldInstantiate(_startShield, enemyTag, new Vector3(-0.4970203f, 0.4541021f, 0.04045296f), new Vector3(-12.426f, 190.746f, 92.01501f));
        }

        _strongAttackSpeed = (transform.TransformDirection(Vector3.forward) * 6f).x;

        _jumpSpeed = 15f;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        UpdateCharacterState(move);
        UpdateAnimator();

        moveDirection = new Vector3(move * _speed, moveDirection.y, 0);
        WalkAnimation(move);

        if (_controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = _jumpForce;
                _animator.SetTrigger("Jump");
            }
        }
        else
        {
            AirBehavior();
        }

        if (!_controller.isGrounded)
            moveDirection.y = moveDirection.y + (Physics.gravity.y * _jumpGravity * Time.deltaTime);
        _controller.Move(moveDirection * Time.deltaTime);

        LockZAxis();
    }

    private void UpdateCharacterState(float move)
    {
        _climbing = _animator.GetCurrentAnimatorStateInfo(0).fullPathHash == climbingState;

        if (_weapon != null)
            _attacking = battleAnimation.IsAttacking(_animator, _weapon);

        // Rotação do personagem
        if (move != 0 && !_climbing && !_attacking)
        {
            Vector3 NextDir = new Vector3(move, 0, 0);
            if (NextDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(NextDir);
        }

        _isAttackPressed = Input.GetButtonDown("WeakAttack");
        _isStrongAttackPressed = Input.GetButtonDown("StrongAttack");
        _isDefenseHold = Input.GetButton("Defense");
    }

    private void UpdateAnimator()
    {
        // Está com os pés no chão?
        _grounded = _controller.isGrounded;
        // Verifica se o player está caindo
        _falling = (_controller.velocity.y <= -0) ? !IsNextToFloor(0.5f) : false;
        // Avisa ao animator se o player está caindo, próximo ao chão ou se está no chão
        _animator.SetBool("Falling", _falling);
        _animator.SetBool("nextToFloor", false);
        _animator.SetBool("Grounded", _grounded);

        if (_weapon != null)
            _isFinishingAttack = battleAnimation.IsFinishingAttack(_animator, _weapon);
        _animator.SetBool("Finishing Attack", (_isFinishingAttack > 0) ? true : false);
    }

    private void WalkAnimation(float move)
    {
        //Verifica para que lado o jogador pressionou.
        if (move < 0)
            move *= -1;
        _animator.SetFloat("Speed", move);
    }

    // Esta função cuida do personagem enquanto ele estiver no ar
    private void AirBehavior()
    {
        // Se estiver numa superfície escalável, quiser escalar e o estado atual não é escalando
        if (!_animator.GetBool("Falling") && !_animator.GetBool("nextToFloor"))
            _animator.SetBool("nextToFloor", IsNextToFloor(2));
    }

    private void BattleBehavior()
    {
        if (_isAttackPressed && _weapon != null && !_animator.GetBool("Attacking"))
        {
            if (_isFinishingAttack == 2)
                _animator.SetTrigger("Attack 3");
            else if (_isFinishingAttack == 1)
                _animator.SetTrigger("Attack 2");
            else if (!_attacking)
                _animator.SetTrigger("Attack");
            if (_isFinishingAttack == 1 || _isFinishingAttack == 2 || !_attacking)
                WeakAttack();
            int stage = (_isFinishingAttack > 0) ? _isFinishingAttack : 0;
        }
        else if (_isStrongAttackPressed && _weapon != null && (!_attacking || _isFinishingAttack == 1 || _isFinishingAttack == 2))
        {
            StrongAttack();
            _animator.SetTrigger("Attack Strong");
        }
    }

    public void WhenStartingAttack()
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        if (IsAttackingOrDefending() && !state.IsTag("Attack Strong"))
        {
            float l = Mathf.Lerp(_controller.velocity.x / 1.75f, 0, Time.deltaTime);
            moveDirection.x = l;
            _animator.SetFloat("Speed", moveDirection.x);
        }
        else if (_attacking && state.IsTag("Attack Strong"))
        {
            float l = 0;
            if (_controller.velocity.x != 0)
                l = Mathf.Lerp(_controller.velocity.x, 0, Time.deltaTime);
            else
            {
                l = Mathf.Lerp(_strongAttackSpeed, 0, Time.deltaTime);
                _strongAttackSpeed = l;
            }
            moveDirection.x = l;
        }
        else
        {
            _strongAttackSpeed = (_controller.transform.TransformDirection(Vector3.forward) * 6f).x;
        }
    }

    private void ArrowBehavior()
    {
        bool hasArrow = (arrow != null) ? true : false;
        if ((Input.GetButtonDown("Shoot") || Input.GetAxis("Shoot") > 0.1f) && !hasArrow)
        {
            arrow = Instantiate(arrowModel, bowHolder.transform.position, bowHolder.transform.rotation);
            arrow.GetComponent<ArrowBahaviour>().SetOwner(this);
            arrow.transform.parent = bowHolder.transform;
        }
        else if ((Input.GetButtonDown("Shoot") || Input.GetAxis("Shoot") > 0.1f) && hasArrow && arrow.GetComponent<ArrowBahaviour>() != null)
        {
            Vector3 vec3byport = new Vector3(Input.GetAxis("HoriLJ"), Input.GetAxis("VertLJ"));
            vec3byport = (vec3byport == Vector3.zero) ? MathematicalSupport.ViewPortNormatize() : vec3byport;
            arrow.transform.rotation = Quaternion.Euler(-MathematicalSupport.ZRotation(vec3byport), 90, 0);
            arrow.GetComponent<ArrowBahaviour>().charge();
        }
        else if ((Input.GetButtonDown("Shoot") || Input.GetAxis("Shoot") <= 0.1f) && hasArrow && arrow.GetComponent<ArrowBahaviour>() != null)
        {
            arrow.GetComponent<ArrowBahaviour>().fire();
            arrow.transform.parent = null;
            arrow = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        StatusEnteringTrigger(other);
        TryTrigger(other.gameObject, true, gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        StatusExitingTrigger(other);
    }

    void TryTrigger(GameObject other, bool enter, GameObject sender)
    {
        ITriggerable triggerable = other.GetComponent<ITriggerable>();
        if (triggerable != null)
            triggerable.Trigger(enter, sender);
        ICheckpoint checkpoint = other.GetComponent<ICheckpoint>();
        if (checkpoint != null)
        {
            this.RestoreAllStatus();
            _checkpoint = checkpoint.Checkpoint();
        }
    }

    private bool IsAttackingOrDefending()
    {
        return _attacking || _isDefenseHold;
    }

    protected void RestoreToCheckpoint(Transform checkpoint)
    {
        if (isDead)
        {
            RestoreAllStatus();
            if (checkpoint != null)
                transform.position = checkpoint.position;
            else
                transform.position = GameObject.Find("Start Point").transform.position;
        }
    }
}
