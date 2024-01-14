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
	private Vector3 camSun;   // взаємне розташування Камера-Сонце
	private Camera _camera;   // "наш" компонент Камера

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
		float my = sensitivityVertical * Input.GetAxis("Mouse Y");  // !! Це НЕ координати, це
		float mx = sensitivityHorizontal * Input.GetAxis("Mouse X");  // зміщення (delta-X)
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
			// обмежити зміни кута зору [5..120]
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
/* Управління камерою
 * - камера - це звичайний ГО із відповідним компонентом (камера)
 *    переміщення та повороти ГО впливають на інформацію від камери,
 *    масштабування - ні.
 * - ефект наближення/віддалення реалізуєтья не через ГО, а через
 *    його компонент (камера) зміною кута поля зору (field of view)
 * - впровадження змін у камеру бажано робити у події LateUpdate
 * 
 * = Rotate(-my, mx, 0); - неправильний підхід, оскільки передбачає
 *    одночасне обертання по двох осях (повертати повернуте), що наслідком
 *    призводить до повороту і по третій осі.
 *    
 *    Д.З. Робота з камерою
 *    Реалізувати можливість повернення камери у початкову 
 *    позицію (координати, повороти та кут зору) за натисненням
 *    деякої клавіші (ESC, Space, - на вибір)
 */
