using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsScript : MonoBehaviour
{
	[SerializeField]
	public GameObject sun;  // Reference to the sun object
	public float rotationSpeed = 10f;  // Speed of rotation

	private float orbitRadius;  // Distance from the sun

	void Start()
    {
		orbitRadius = Vector3.Distance(this.transform.position, sun.transform.position);
	}

    // Update is called once per frame
    void Update()
    {
		// Calculate the rotation angle based on time and rotation speed
		float rotationAngle = rotationSpeed * Time.deltaTime;

		// Rotate the planet around the sun
		this.transform.RotateAround(sun.transform.position, Vector3.up, rotationAngle);

		// Update the position of the planet after rotation
		Vector3 desiredPosition = (this.transform.position - sun.transform.position).normalized * orbitRadius + sun.transform.position;
		transform.position = Vector3.MoveTowards(this.transform.position, desiredPosition, rotationSpeed * Time.deltaTime);
	}
}
