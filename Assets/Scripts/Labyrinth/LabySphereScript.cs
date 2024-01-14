using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabySphereScript : MonoBehaviour
{
	[SerializeField]
	private GameObject _camera;
	[SerializeField]
	private GameObject cameraAnchor1;

	private Rigidbody body;
	private float forceFactor = 400f;
	private Vector3 anchorOffset;

	private AudioSource backgroundMusic;
	private AudioSource collectSoundEffect;

	private static LabySphereScript _instance;

	void Start()
	{
		if (_instance != null)
		{
			this.transform.position += new Vector3(0, _instance.transform.position.y, 0);
			GameObject.Destroy(_instance.gameObject);
		}
		_instance = this;
		DontDestroyOnLoad(this.gameObject);

		body = GetComponent<Rigidbody>();
		anchorOffset = cameraAnchor1.transform.position - this.transform.position;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		collectSoundEffect = audioSources[0];
		backgroundMusic = audioSources[1];

		// backgroundMusic.Play();
	}

	void Update()
	{
		float av = Input.GetAxis("Vertical");
		float ah = Input.GetAxis("Horizontal");

		Vector3 right = _camera.transform.right;
		Vector3 forward = _camera.transform.forward;
		forward.y = 0;
		forward.Normalize();
		Vector3 moveDirection = ah * right + av * forward;
		body.AddForce(Time.deltaTime * forceFactor * moveDirection);

		cameraAnchor1.transform.position = anchorOffset + this.transform.position;
	}
}
