using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // speed
    // Gravity
    // Direction
    // Jump height

    private CharacterController _controller;
    private Vector3 _direction;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    [SerializeField] private float _speed = 15.0f;
    private bool _jumping = false;
    private Animator _anim;
    private bool _onLedge = false;
    private Vector3 _standPos;
    private Ledge _activeLedge;
 private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.E) && _onLedge == true)
        {
            _anim.SetTrigger("ClimbUp");
        }
    }
    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }
            
            var h= Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(h));

            if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }
        _direction.y -= _gravity;
        _controller.Move(_direction * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge",true);
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("Speed", 0.0f);
        transform.position = handPos;
        _onLedge = true;
        _activeLedge = currentLedge;
    }

    public void ClimbComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
    }
}
