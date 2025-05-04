using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC2 : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float rY;
	[SerializeField] private float rX;
	[SerializeField] private float mouseSensitivity = 100f;
	
	private void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal") * speed;
		float vertical = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(horizontal, 0f, vertical);


		transform.GetChild(0).eulerAngles = new Vector3(rX, rY, 0);

		if (movement != Vector3.zero)
		{
			movement = Quaternion.Euler(0f, rY, 0f) * movement;
			rb.AddForce(movement);
		}
	}

	private void Update(){
		rY += Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
		rX -= Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;

		rY %= 360;
		rX %= 360;
	}


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		LockCursor();
	}

	private void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}