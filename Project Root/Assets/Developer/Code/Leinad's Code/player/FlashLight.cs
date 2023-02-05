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
        float percentBatery = _batery / maxtime;
        
        
        if (Time.time <= maxtime/2)
        {
            
            _flashLightPlayer.intensity = _angle;
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
            Debug.Log("se acabo la bateria");
        }
    }

}
