using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Pathfinding, IMoveInterface, ITriggerable
{
    [SerializeField]
    Transform[] _patrols;

    private Transform _object;
    private Rigidbody _rigibody;
    private Animator _animator;
    private Vector3 _actualLocal, _destination;
    private GameObject _player;
    private int _patrolNumber = 0;
    private bool _chasing = false;

    // Use this for initialization
    void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        if (_rigibody == null)
        {
            Debug.Log("There's no rigibody.");
        }
        if (_animator == null)
        {
            Debug.Log("There's no animator.");
        }

        UpdatePatrol();
        NewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = _rigibody.velocity.y;
        if (moveSpeed < 0)
            moveSpeed *= -1;
        if (AtDestination() || _chasing)
        {
            moveSpeed = 0;
            UpdatePatrol();
            NewDestination();
        }
        else
            _actualLocal = _rigibody.position;
        _animator.SetFloat("Speed", moveSpeed);
    }

    public bool AtDestination()
    {
        if (_actualLocal.x == _destination.x)
            return true;
        return false;
    }

    public void NewDestination()
    {
        _actualLocal = gameObject.transform.position;
        Vector3 newDestination = new Vector3(_object.position.x,
                                        _rigibody.position.y,
                                        _rigibody.position.z);
        _destination = newDestination;
        base.FollowTransform(_destination);
    }

    public void UpdatePatrol()
    {
        if(_chasing)
        {
            _object = _player.transform;
            return;
        }
        if (_patrolNumber == _patrols.Length)
            _patrolNumber = 0;
        if (_object == null || _object.tag == "Player")
        {
            _object = _patrols[_patrolNumber];
            return;
        }
        _object = _patrols[_patrolNumber];
        _patrolNumber++;
    }

    public void Trigger(bool enter, Object other)
    {
        if (enter)
        {
            _chasing = true;
            _player = GameObject.Find(other.name);
            UpdatePatrol();
            NewDestination();
        }
        else
        {
            _chasing = false;
            UpdatePatrol();
            NewDestination();
        }
    }
}
