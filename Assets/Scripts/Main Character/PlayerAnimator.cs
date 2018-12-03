using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void isJumping(bool jump)
    {
        _animator.SetBool("isJumping", jump);
    }
    public void isMoving(bool jump)
    {
        _animator.SetBool("isMoving", jump);
    }
}
