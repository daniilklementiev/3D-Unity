using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabySpotLightScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    
    private Light _light;
    
    private float batteryLevel = 1.0f; 
    private float dischargeRate = 0.2f;
    
    void Start()
    {
        _light = GetComponent<Light>();
    }
    
    
    void Update()
    {
    	if (LabyState.isPaused) return;
    
    	if (LabyState.firstPersonView)
        if (LabyState.isDay)
        {
            _light.intensity = 0f;
            batteryLevel = 1.0f;
    
        }
        else
        {
            _light.intensity = 1.0f;
            this.transform.position = sphere.transform.position;
            this.transform.forward = Camera.main.transform.forward;
            Vector2 wheel = Input.mouseScrollDelta;
            if(wheel.y != 0)
            {
                float angle = Mathf.Clamp(_light.spotAngle + wheel.y, 20f, 70f);
                if(angle != _light.spotAngle)
                {
    				_light.spotAngle = angle;
                    _light.intensity = 1 - (angle - 50f) / 30f;
    			}
    		}
            if(_light.intensity == 0f)
            {
    			_light.intensity = 1 - (_light.spotAngle - 50f) / 30f;
    		}
    
            batteryLevel -= dischargeRate * Time.deltaTime;
            _light.intensity = batteryLevel;
            if(batteryLevel <= 0f)
            {
                LabyState.isDay = true;
            }
        }
    }
}
