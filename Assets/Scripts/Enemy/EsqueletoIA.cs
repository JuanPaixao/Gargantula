using System.Collections;
using UnityEngine;

public class EsqueletoIA : CharacterBehaviour, IEnemy
{
    public AudioClip spawningAudio, spawnAudio, attackAudio, swordAudio, swordComboAudio, swordStrongAudio, shieldAudio;

    GameObject _player;
    EsqueletoAnimation _battleAnimation;
    Vector3 _moveDirection;

    bool _spawned;
    bool _walk, _aggressive, _defensive, _restoringStamina;

    void Start()
    {
        InitializeCharacter();
        if (GetComponentInChildren<StatusCanvas>()) magic = GetComponentInChildren<StatusCanvas>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _spawned = false;
        _battleAnimation = new EsqueletoAnimation();
        _speed = 7f;

        WeaponInstantiate(_weapon, "Player");
        ShieldInstantiate(_shield, "Player", new Vector3(-0.047f, -0.238f, 0.09f), new Vector3(-167.321f, 179.389f, -103.487f));

        _shield.GetComponent<BoxCollider>().enabled = false;
        if(magic) magic.gameObject.SetActive(false);
    }

    void Update()
    {
        if (life <= 0)
            Destroy(this.gameObject);
        if (!_spawned || IsSpawning())
            return;
        UpdateStatus();
        if (_restoringStamina)
            return;
        _walk = false;
        _moveDirection = new Vector3(0, 0, 0);
        if (IsNextToPlayer(3))
        {
            if (stamina > 10)
                Attack();
            else
                Defensive();
        }
        else
        {
            if (life > OLife / 2)
                Aggresive();
            else
                Defensive();
        }
        _animator.SetBool("Walk", _walk);
        _moveDirection.y -= _gravity;
        _controller.Move(_moveDirection * Time.deltaTime);
        LockZAxis();
    }

    void FixedUpdate()
    {
        _attacking = _battleAnimation.IsAttacking(_animator, _weapon);
        _weapon.GetComponent<WeaponBehavior>().IsAttacking(_attacking);
    }

    private void Attack()
    {
        _aggressive = true;
        _defensive = false;
        bool canAttack = true;
        if (life < OLife / 3)
        {
            int possibility = Random.Range(0, 10);
            if (possibility < 6)
                canAttack = false;
        }
        if (stamina > 80 && canAttack)
            AlternativeAttack();
        else if (stamina > 10 && canAttack)
            BaseAttack();
        else if (!canAttack)
            Defensive();
    }

    public void BaseAttack()
    {
        WeakAttack();
        _animator.SetTrigger("Ataque");
        if (stamina > 60)
        {
            WeakAttack();
            _animator.SetTrigger("Combo");
        }
    }

    public void AlternativeAttack()
    {
        StrongAttack();
        _animator.SetTrigger("Ataque forte");
    }

    private void Aggresive()
    {
        _aggressive = true;
        _defensive = false;
        ChasePlayer(DistanceToPlayer());
    }

    private void Defensive()
    {
        _aggressive = false;
        _defensive = true;
        StartCoroutine(RestoreStamina(5));
    }

    private void ChasePlayer(Vector3 distanceToPlayer)
    {
        float move = distanceToPlayer.x;
        float moveNormalized = (move > 0) ? 1 : -1;
        if (move != 0)
        {
            Vector3 NextDir = new Vector3(moveNormalized, 0, 0);
            if (NextDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(NextDir);
        }

        _moveDirection = new Vector3(0, 0, 1);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= _speed;
        _walk = true;
    }

    public IEnumerator RestoreStamina(int seconds)
    {
        _shield.GetComponent<BoxCollider>().enabled = true;
        _animator.SetBool("Defesa", true);
        _restoringStamina = true;
        yield return new WaitForSeconds(seconds);
        _restoringStamina = false;
        _animator.SetBool("Defesa", false);
        _shield.GetComponent<BoxCollider>().enabled = false;
    }

    private Vector3 DistanceToPlayer()
    {
        Vector3 distanceToPlayer = _player.transform.position - transform.position;
        return distanceToPlayer;
    }

    private bool IsSpawning()
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag("Spawning"))
            return true;
        return false;
    }

    private void Spawn()
    {
        transform.LookAt(_player.transform);
        _animator.SetTrigger("Spawn");
        _spawned = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_spawned) Spawn();
        StatusEnteringTrigger(other);
    }

    public void OnSpawning(AnimationEvent animationEvent)
    {
        _audio.PlayOneShot(spawningAudio);
    }

    public void Spawned(AnimationEvent animationEvent)
    {
        _audio.PlayOneShot(spawnAudio);
        magic.gameObject.SetActive(true);
    }

    public void Ataque(AnimationEvent animationEvent)
    {
        _audio.PlayOneShot(attackAudio);
    }

    public bool IsNextToPlayer(int proximity)
    {
        if (Mathf.Abs(DistanceToPlayer().x) < proximity)
            return true;
        return false;
    }

    public bool IsAggressive()
    {
        return _aggressive;
    }

    public bool IsChasing()
    {
        return _walk;
    }
}
