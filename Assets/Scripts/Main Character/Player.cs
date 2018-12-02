using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isOnDialogue = false;
    public float speed, gravity, checkGround, jumpForce;
    public LayerMask groudLayerMask;
    public bool isGrounded;
    public GameObject groundDetector;
    private CharacterController _controller;
    private RaycastHit _hit;
    [HideInInspector] public float movHor, movVer;
    private Vector3 moveDirection = Vector3.zero;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Movement();
        isGrounded = (Physics.Raycast(groundDetector.transform.position, Vector3.down, checkGround, groudLayerMask));
    }
    private void Movement()
    {
        movHor = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        moveDirection = new Vector2(movHor, 0);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpForce;

        }
        if (isGrounded && !Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = 0;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection);

        _transform.transform.eulerAngles = (movHor < 0) ? _transform.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, -109.66f, this.transform.eulerAngles.z) :
        _transform.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 89.71101f, this.transform.eulerAngles.z);
    }
}
