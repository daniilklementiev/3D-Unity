using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyGates2Script : MonoBehaviour
{
    private float activator = 0f;
    private float period = 1f;
	private string[] observedProperties = { nameof(LabyState.key2Activated), nameof(LabyState.key2Collected) };

    void Start()
    {
		LabyState.AddObserver(OnGameStateChanged, observedProperties);
	}


    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * activator / period);
    }

	private void OnGameStateChanged(string propertyName)
	{
		if (propertyName == nameof(LabyState.key2Activated) && LabyState.key2Activated)
		{
			activator = 1f;
		}
		else if (propertyName == nameof(LabyState.key2Collected) && LabyState.key2Collected)
		{ 
			period /= 10f;
		}
	}

	private void OnDestroy()
	{
		LabyState.RemoveObserver(OnGameStateChanged, observedProperties);
	}
}
