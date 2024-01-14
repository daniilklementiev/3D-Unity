using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyLightScript : MonoBehaviour
{
	/*private Light _light;
    private bool _isDay;
    private Color dayLight = Color.white;
    private Color nightLight = Color.blue;
    private bool isDay
    {
        get => _isDay;
        set
        {
            _isDay = LabyState.isDay = value;
            if (_isDay)
            {
                SetDaylighting();
            }
            else
            {
                SetNightlighting();
            }
        }
    }

    private float targetIntensity;
    private float targetExposure;
    private float transitionSpeed = 0.5f; // Adjust this value for the speed of the transition

    void Start()
    {
        _light = GetComponent<Light>();
        isDay = true;
    }

    void Update()
    {
        if (LabyState.isPaused) return;

        if (Input.GetKeyUp(KeyCode.N))
        {
            isDay = !isDay;
        }

        // Adjust the target intensity based on user input
        targetIntensity += Input.GetKeyDown(KeyCode.Equals) ? 0.1f : 0f; // "+" key
        targetIntensity -= Input.GetKeyDown(KeyCode.Minus) ? 0.1f : 0f; // "-" key
        targetIntensity = Mathf.Clamp(targetIntensity, 0.01f, 1.0f);

        // Smoothly transition the light intensity
        _light.intensity = Mathf.Lerp(_light.intensity, targetIntensity, Time.deltaTime * transitionSpeed);

        // Smoothly transition the skybox exposure
        float currentExposure = RenderSettings.skybox.GetFloat("_Exposure");
        targetExposure += Input.GetKeyDown(KeyCode.Equals) ? 0.1f : 0f; // "+" key
        targetExposure -= Input.GetKeyDown(KeyCode.Minus) ? 0.1f : 0f; // "-" key
        targetExposure = Mathf.Clamp(targetExposure, 0.01f, 1.0f);
        float newExposure = Mathf.Lerp(currentExposure, targetExposure, Time.deltaTime * transitionSpeed);
        RenderSettings.skybox.SetFloat("_Exposure", newExposure);
    }

    private void SetDaylighting()
    {
        targetIntensity = 1f;
        targetExposure = 1f;
        _light.color = dayLight;
    }

    private void SetNightlighting()
    {
        targetIntensity = 0.01f;
        targetExposure = 0.01f;
        _light.color = nightLight;
    }

    private void OnDestroy()
    {
        isDay = true;
    }*/

	private Light _light;
    private bool  _isDay;
    private Color dayLight = Color.white;
    private Color nightLight = Color.blue;
    private bool  isDay {
        get => _isDay;
    	set
        {
			_isDay = LabyState.isDay = value;
			if (_isDay)
            {
				SetDaylighting();
			}
			else
            {
				SetNightlighting();
			}
		}
	}

    void Start()
    {

        _light = this.GetComponent<Light>();
        isDay = true;
    }

    
    void Update()
    {
        if (LabyState.isPaused) return;
        if(Input.GetKeyUp(KeyCode.N))
        {
            isDay = !isDay;
        }
    }

    private void SetDaylighting()
    { 
        _light.intensity = 1f;
        _light.color = dayLight;
        RenderSettings.skybox.SetFloat("_Exposure", 1f);
    }

    private void SetNightlighting()
    {
        _light.intensity = .01f;
		_light.color = nightLight;
        RenderSettings.skybox.SetFloat("_Exposure", .01f);
	}

	private void OnDestroy()
	{
        isDay = true;
	}
}
