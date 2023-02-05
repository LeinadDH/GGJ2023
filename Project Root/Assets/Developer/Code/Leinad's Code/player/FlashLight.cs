using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.Rendering.Universal;
using TMPro;

public class FlashLight : MonoBehaviour
{
    private Light2D _flashLightGlow;
    private Light2D _flashLightPlayer;
    private float _batery = 100;
    private float _currentTime = 0.0f;
    float currentTimeTwo = 0.0f;
    private float _angle = 1;
    private float _radius = 6;
    public TMP_Text _textMeshPro;
    public SphereCollider PlayerCollider;

    public void GetLights()
    {
        foreach (var lights in GetComponentsInChildren<Light2D>(true))
        {
            if (lights.name == "FlashLight")
            {
                _flashLightGlow = lights.GetComponent<Light2D>();
            } 
 
            if (lights.name == "PlayerLight")
            {
                _flashLightPlayer = lights.GetComponent<Light2D>();
            }
        }
    }

    public void FlashLightTimer(float maxtime)
    {
        _currentTime += Time.deltaTime;
        currentTimeTwo += Time.deltaTime;
        float radiuspercent = 6f / maxtime;
        float anglepercent = 1f / maxtime;
        float percentBatery = 100 / maxtime *2;
        
        
        if (Time.time <= maxtime/2)
        {
            
            _flashLightPlayer.pointLightOuterRadius = _angle;
            _flashLightGlow.pointLightOuterRadius = _radius;
            
            if(currentTimeTwo >= 1)
            {
                _batery = _batery - percentBatery;
                _textMeshPro.text = Math.Round(_batery).ToString() + "%";
                currentTimeTwo = 0.0f;
            }

            if (_currentTime >= 0.5f)
            {
                _angle = _angle - anglepercent;
                _radius = _radius - radiuspercent;
                _currentTime = 0.0f;
            } 
        }
        else
        {
            _textMeshPro.text = "0%";
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SmallBatery")
        {
            _batery = _batery + 25;
            _radius = _radius + 1.5f;
            _angle = _angle + 0.25f;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "BigBatery")
        {
            _batery = _batery + 50;
            _radius = _radius + 3f;
            _angle = _angle + 0.5f;
            collision.gameObject.SetActive(false);
        }
    }
}
