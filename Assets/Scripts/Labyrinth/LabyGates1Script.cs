using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyGates1Script : MonoBehaviour
{
    private float period = 3f;


    void Start()
    {
        
    }

    void Update()
    {
        float factor = Time.deltaTime / period;
        if(!LabyState.key1Collected)
        {
			factor *= 0.02f;
		}
        Vector3 newPosition = this.transform.position + factor * Vector3.down;

        if(newPosition.y < -0.16f || newPosition.y > 0.16f)
        {
            period = -period;
            newPosition.y = newPosition.y < -0.16f ? -0.16f : 0.16f;
		}
        this.transform.position = newPosition;
    }
}
