using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabyKey1Script : MonoBehaviour
{
    [SerializeField]
    private Image indicator;
    private float period = 10f;

    void Start()
    {
        LabyState.key1Remained = 1f;
        LabyState.key1Collected = false;
    }


    void Update()
    {
		LabyState.key1Remained -= Time.deltaTime / period;
        if(LabyState.key1Remained < 0f)
        {
            GameObject.Destroy(this.gameObject);
		}
        else
        {
            indicator.fillAmount = LabyState.key1Remained;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		LabyState.key1Collected = true;
        GameObject.Destroy(this.gameObject);
	}
}
