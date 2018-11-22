using UnityEngine;

public class NPCMove : Pathfinding, IMoveInterface
{
    [SerializeField]
    Transform _floor;

    private Rigidbody _rigibody;
    private Animator _animator;
    private Vector3 _actualLocal, _destination;

    // Use this for initialization
    void Start () {
        _rigibody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        if(_rigibody == null)
        {
            Debug.Log("There's no rigibody.");
        }
        if(_animator == null)
        {
            Debug.Log("There's no animator.");
        }

        NewDestination();
	}
	
	// Update is called once per frame
	void Update () {
        float moveSpeed = _rigibody.velocity.y;
        if (moveSpeed < 0)
            moveSpeed *= -1;
        if (AtDestination())
        {
            moveSpeed = 0;
            NewDestination();
        }
        else
        {
            _actualLocal = _rigibody.position;
        }
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
        Mesh planeMesh = _floor.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        Vector3 newDestination = new Vector3(Random.Range(minX, -minX),
                                        _rigibody.position.y,
                                        _rigibody.position.z);
        _destination = newDestination;
        base.FollowTransform(_destination);
    }
}
