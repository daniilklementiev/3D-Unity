using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FobosScript : MonoBehaviour
{
	[SerializeField]
	public GameObject mars;  // Reference to the Mars object
	public GameObject sun;   // Reference to the Sun object
	public float rotationSpeed = 10f;  // Speed of rotation

	private float marsOrbitRadius;  // Distance from Mars
	private float sunOrbitRadius;   // Distance from Sun to Mars

	void Start()
	{
		// Calculate the initial orbit radii based on the distances from Mars to the satellite and Sun to Mars
		marsOrbitRadius = Vector3.Distance(transform.position, mars.transform.position);
		sunOrbitRadius = Vector3.Distance(mars.transform.position, sun.transform.position);
	}

	void Update()
	{
		// Calculate the rotation angle based on time and rotation speed
		float rotationAngle = rotationSpeed * Time.deltaTime;

		// Rotate the satellite around Mars
		transform.RotateAround(mars.transform.position, Vector3.up, rotationAngle);

		// Update the position of the satellite after Mars rotation
		Vector3 marsDesiredPosition = (transform.position - mars.transform.position).normalized * marsOrbitRadius + mars.transform.position;
		transform.position = Vector3.MoveTowards(transform.position, marsDesiredPosition, rotationSpeed * Time.deltaTime);

		// Rotate Mars around the Sun
		mars.transform.RotateAround(sun.transform.position, Vector3.up, rotationAngle);

		// Update the position of Mars after Sun rotation
		Vector3 sunDesiredPosition = (mars.transform.position - sun.transform.position).normalized * sunOrbitRadius + sun.transform.position;
		mars.transform.position = Vector3.MoveTowards(mars.transform.position, sunDesiredPosition, rotationSpeed * Time.deltaTime);
	}
}
