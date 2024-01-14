using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;

    private GameObject surface;
    private GameObject atmosphere;
    private float dayPeriod = 24f / 360f;
    private float skyPeriod = 36f / 360f;
    private float yearPeriod = 73f / 360;

    void Start()
    {
        surface = GameObject.Find("EarthSurface");
        atmosphere = GameObject.Find("EarthAtmosphere");
    }

    void Update()
    {
        surface.transform.Rotate(Vector3.up, Time.deltaTime / dayPeriod, Space.Self);
        atmosphere.transform.Rotate(Vector3.up, Time.deltaTime / skyPeriod, Space.Self);
        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
