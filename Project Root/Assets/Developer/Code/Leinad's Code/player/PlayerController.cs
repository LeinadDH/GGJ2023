using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Move move;
    private FlashLight flashLight;
    public float MaxLighTime;
    public TMP_Text textMeshProUGUI;

    private void Awake()
    {
        move = this.gameObject.GetComponent<Move>();
        flashLight = this.gameObject.GetComponent<FlashLight>();
        flashLight.GetLights();
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
        flashLight.FlashLightTimer(MaxLighTime);
        
    }
}
