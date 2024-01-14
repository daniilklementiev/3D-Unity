using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabyKey2Script : MonoBehaviour
{
	[SerializeField]
	private Image indicator;
	private float period = 10f;
	private float activeFactor = 0f;
	void Start()
	{
		LabyState.AddObserver(OnGameStateChanged, nameof(LabyState.key2Activated));
		LabyState.key2Remained = 1f;
		LabyState.key2Collected = false;
	}

	void Update()
	{
		LabyState.key2Remained -= Time.deltaTime / period * activeFactor;
		if (LabyState.key2Remained < 0f)
		{
			GameObject.Destroy(this.gameObject);
		}
		else
		{
			indicator.fillAmount = LabyState.key2Remained;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		LabyState.key2Collected = true;
		GameObject.Destroy(this.gameObject);
	}

	private void OnDestroy()
	{
		LabyState.RemoveObserver(OnGameStateChanged, nameof(LabyState.key2Activated));
	}

	private void OnGameStateChanged(string propertyName)
	{
		if(propertyName == nameof(LabyState.key2Activated) && LabyState.key2Activated)
		{
			activeFactor = 1f;
		}
	}
}
