using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC2 : MonoBehaviour
{
	public int speed;
	public Rigidbody rb;
	public bool isGrounded;
	public float jumpForce;
	public bool IsStatic;
	public MSCamera MyCamera;
	//public Storage st;
	
	public float rotationY = 0f;
	
	private void FixedUpdate()
	{
		if(!IsStatic){
			MovePlayer();
		}
	}

	private void MovePlayer()
	{
		float horizontal = Input.GetAxis("Horizontal") * speed;
		float vertical = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(horizontal, 0f, vertical);

		if (movement != Vector3.zero)
		{
			movement = Quaternion.Euler(0f, rotationY, 0f) * movement;
			rb.AddForce(movement);
		}
	}

	public float mouseSensitivity = 100f;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		LockCursor();
		//transform.position = st.PlayerPos3D;
		//Gm = GameObject.FindGameObjectWithTag("Respawn").GetComponent<GameManager>();
	}

	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		RotatePlayerWithMouse();
		CheckJumpInput();
	}

	/*
	private void RotatePlayerWithMouse()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		rotationY += mouseX;
		transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
		//MyCamera.ry = rotationY;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UnlockCursor();
		}
	}
	*/

	private void RotatePlayerWithMouse()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		rotationY += mouseX;

		Vector3 currentRotation = transform.rotation.eulerAngles;
		Vector3 globalRotation = new Vector3(0f, rotationY, 0f);
		globalRotation += new Vector3(MyCamera.transform.rotation.x, 0f, MyCamera.transform.rotation.z);

		transform.rotation = Quaternion.Euler(globalRotation);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UnlockCursor();
		}
	}


	public void UnlockCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void CheckJumpInput()
	{
		if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
			rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
		}
	}
}