using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Move move;

    private void Awake()
    {
        move = this.gameObject.GetComponent<Move>();
        move.SetComponents();
    }

    private void OnEnable()
    {
        move.PlayerInput.Enable();
    }

    private void OnDisable()
    {
        move.PlayerInput.Disable();
    }

    void FixedUpdate()
    {
        move.MovePlayer();
    }
}
