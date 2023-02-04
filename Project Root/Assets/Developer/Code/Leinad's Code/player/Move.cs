using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private PlayerActions _playerInput;
    private Rigidbody _rb;
    private float speed = 10f;

    public PlayerActions PlayerInput { get => _playerInput; set => _playerInput = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value;}
    public float Speed { get => speed;set => speed = value; }

    public void SetComponents() 
    {
        _playerInput = new PlayerActions();
        _rb = GetComponent<Rigidbody>();
    }

    public void MovePlayer()
    {
        Vector3 moveInput = _playerInput.Movement.Move.ReadValue<Vector3>();
        _rb.velocity = moveInput * speed;
    }
}
