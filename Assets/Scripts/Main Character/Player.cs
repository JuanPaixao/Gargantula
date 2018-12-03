using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isOnDialogue = false;
    private bool _isJumping;
    public int numberOfJumps, maxJumps;
    public float speed, gravity, checkGround, jumpForce;
    public LayerMask groudLayerMask;
    public bool isGrounded;
    public GameObject groundDetector;
    private Rigidbody _rb;
    private RaycastHit _hit;
    [HideInInspector] public float movHor, movVer;
    private Vector3 movement;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        maxJumps = 2;
    }
    void Update()
    {
        isGrounded = (Physics.Raycast(groundDetector.transform.position, Vector2.down, out _hit, checkGround, groudLayerMask));
        if (isGrounded)
        {
            _isJumping = false;
            numberOfJumps = 0;
        }
        MovementInputs();
    }
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * Time.fixedDeltaTime);
    }
    private void MovementInputs()
    {
        movHor = Input.GetAxis("Horizontal") * speed;
        movement = new Vector3(movHor, 0, 0);
        _transform.transform.eulerAngles = (movHor < 0) ? _transform.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, -109.66f, this.transform.eulerAngles.z) :
        _transform.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 89.71101f, this.transform.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numberOfJumps < maxJumps || isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y), ForceMode.VelocityChange);
                _isJumping = true;
                numberOfJumps++;
            }
        }
    }
}
