using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicScript : MonoBehaviour
{
    private static AmbientMusicScript _instance;

    void Start()
    {
        if (_instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            // save the keys states too 
            GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
		// ����� 3 ������� ����� ������� ����� ��������� ������� �� LabyrinthScene
        if (Time.timeSinceLevelLoad > 3 && Application.loadedLevelName == "SolarSystemScene")
        {
			Application.LoadLevel("LabyrinthScene");
		}

	}
}
