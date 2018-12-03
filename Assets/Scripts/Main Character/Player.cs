using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int numberOfJumps, maxJumps;
    public float speed, gravity, checkGround, jumpForce;
    public string direction;
    public LayerMask groudLayerMask;
    public bool isGrounded, isMoving, isJumping, canMove;
    public GameObject groundDetector;
    private Rigidbody _rb;
    private RaycastHit _hit;
    [HideInInspector] public float movHor, movVer;
    private Vector3 movement;
    private Transform _transform;
    private PlayerAnimator _playerAnimator;
    private void Awake()
    {
        canMove = true;
        _playerAnimator = GetComponent<PlayerAnimator>();
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        maxJumps = 2;
    }
    void Update()
    {
        if (canMove)
        {
            isGrounded = (Physics.Raycast(groundDetector.transform.position, Vector2.down, out _hit, checkGround, groudLayerMask));
            isMoving = (movHor != 0) ? true : false;
            _playerAnimator.isJumping(isJumping);
            if (movHor > 0)
            {
                direction = "right";
            }
            else if (movHor < 0)
            {
                direction = "left";
            }

            if (isGrounded)
            {
                isJumping = false;
                numberOfJumps = 0;
            }
            MovementInputs();
            _playerAnimator.isMoving(isMoving);
        }
        else
        {
            isMoving = false;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _playerAnimator.isMoving(isMoving);
            movement = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * Time.fixedDeltaTime);

    }
    private void MovementInputs()
    {
        movHor = Input.GetAxis("Horizontal") * speed;
        movement = new Vector3(movHor, 0, 0);
        _transform.transform.eulerAngles = (direction == "left") ? _transform.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, -109.66f, this.transform.eulerAngles.z) :
        _transform.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 89.71101f, this.transform.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numberOfJumps < maxJumps || isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y), ForceMode.VelocityChange);
                isJumping = true;
                numberOfJumps++;
            }
        }
    }
}

