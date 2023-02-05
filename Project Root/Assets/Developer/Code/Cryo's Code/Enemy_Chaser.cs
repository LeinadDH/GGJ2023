using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chaser : Enemy
{
    private bool _kill;
    private void Start()
    {
        FlashLight playerLight = player.GetComponentInChildren<FlashLight>();
        playerLight.OnHalfBattery += PlayerLightOnOnHalfBattery;
        playerLight.OnNoBattery += PlayerLightOnNoBattery;

    }

    protected override void Update()
    {
        base.Update();
        KillMode();
    }

    protected override void OnTriggerStay(Collider other)
    {
        Debug.Log("triggerstay");
        base.OnTriggerStay(other);
        Debug.Log("base done");
        _kill = false;
    }

    void KillMode()
    {
        if (!_kill) return;
        _pursue = true;
        _agent.speed = pursuitSpeed;
    }

    private void PlayerLightOnOnHalfBattery(object sender, EventArgs e)
    {
        _pursue = true;
    }

    private void PlayerLightOnNoBattery(object sender, EventArgs e)
    {
        _kill = true;

    }
}
