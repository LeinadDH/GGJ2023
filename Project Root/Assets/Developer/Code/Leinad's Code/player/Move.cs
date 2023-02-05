using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private AudioManager _manager;
    private PlayerActions _playerInput;
    private Rigidbody _rb;
    [SerializeField]private float speed = 10f;
    public Animator anim;

    public PlayerActions PlayerInput { get => _playerInput; set => _playerInput = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value;}
    public float Speed { get => speed;set => speed = value; }

    private void Start()
    {
        try
        {
            _manager = FindObjectOfType<AudioManager>();
        }
        catch { Debug.Log("Could not find audioManager"); }

        StartCoroutine(CorWaitForAudio());
    }

    public void SetComponents() 
    {
        _playerInput = new PlayerActions();
        _rb = GetComponent<Rigidbody>();
    }

    void PlayAudio()
    {
        if (_rb.velocity.magnitude < .3f) return;
        _manager.PlayOneShot("Step");
    }

    IEnumerator CorWaitForAudio()
    {
        while (true)
        { 
            PlayAudio();
            yield return new WaitForSeconds(.3f);
        }
    }

    public void MovePlayer()
    {
        Vector3 moveInput = _playerInput.Movement.Move.ReadValue<Vector3>();
        _rb.velocity = moveInput * speed;

        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.z);
        anim.SetFloat("Speed", moveInput.sqrMagnitude);
    }
}
