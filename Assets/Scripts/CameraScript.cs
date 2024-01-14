using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[SerializeField]
	private GameObject sun;

	private float camAngleX;
	private float camAngleY;
	private float camSunAngleX;
	private float camSunAngleY;
	private float sensitivityHorizontal = 4f;
	private float sensitivityVertical = 3f;
	private Vector3 camSun;   // ������ ������������ ������-�����
	private Camera _camera;   // "���" ��������� ������

	void Start()
	{
		Debug.Log("CameraScript: " + LabyState.key1Remained);
		camAngleX = transform.eulerAngles.x;
		camAngleY = transform.eulerAngles.y;
		camSun = this.transform.position - sun.transform.position;
		camSunAngleX = camSunAngleY = 0f;
		_camera = GetComponent<Camera>();
	}
	void Update()
	{
		float my = sensitivityVertical * Input.GetAxis("Mouse Y");  // !! �� �� ����������, ��
		float mx = sensitivityHorizontal * Input.GetAxis("Mouse X");  // ������� (delta-X)
		camAngleX -= my;
		camAngleY += mx;
		if (Input.GetMouseButton(0))
		{
			Cursor.lockState = CursorLockMode.Locked;
			camSunAngleX -= my;
			camSunAngleY += mx;
		}
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			// start coordinates
			camAngleX = 0f;
			camAngleY = 0f;
			camSunAngleX = 0f;
			camSunAngleY = 0f;
			_camera.fieldOfView = 60f;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}
	private void LateUpdate()
	{
		this.transform.eulerAngles = new Vector3(camAngleX, camAngleY, 0);
		this.transform.position = sun.transform.position +
			Quaternion.Euler(camSunAngleX, camSunAngleY, 0) * camSun;

		Vector2 wheel = Input.mouseScrollDelta;
		if (wheel != Vector2.zero)
		{
			// �������� ���� ���� ���� [5..120]
			var fov = _camera.fieldOfView - wheel.y;

			if (fov >= 5 && fov <= 120)
			{
				_camera.fieldOfView = fov;
			}
			// _camera.fieldOfView -= wheel.y;
		}
		if (Input.GetMouseButton((int)MouseButton.Right))
		{
			this.transform.Translate(
			   Time.deltaTime * Input.GetAxis("Vertical") * this.transform.forward
				+ Time.deltaTime * Input.GetAxis("Horizontal") * this.transform.right);

			camSun = this.transform.position - sun.transform.position;
		}
	}
}
/* ��������� �������
 * - ������ - �� ��������� �� �� ��������� ����������� (������)
 *    ���������� �� �������� �� ��������� �� ���������� �� ������,
 *    ������������� - �.
 * - ����� ����������/��������� ��������� �� ����� ��, � �����
 *    ���� ��������� (������) ����� ���� ���� ���� (field of view)
 * - ������������ ��� � ������ ������ ������ � ��䳿 LateUpdate
 * 
 * = Rotate(-my, mx, 0); - ������������ �����, ������� ���������
 *    ��������� ��������� �� ���� ���� (��������� ���������), �� ��������
 *    ���������� �� �������� � �� ����� ��.
 *    
 *    �.�. ������ � �������
 *    ���������� ��������� ���������� ������ � ��������� 
 *    ������� (����������, �������� �� ��� ����) �� �����������
 *    ����� ������ (ESC, Space, - �� ����)
 */
