using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public int test;
	public string text;
	private Rigidbody rb;
	public float speed;
	public float up;
	public float mouseSensitivity;
	private float rotationY;
	private float rotationX;

	// Start is called before the first frame update
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		//LockCursor();
	}

	// Update is called once per frame
	void Update()
	{
		if(!Cursor.visible) {
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
			rotationY += mouseX;
			rotationX -= mouseY;
			transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
			if (Input.GetKey(KeyCode.Space)) {
				transform.position += new Vector3(0f, up, 0f);
			}
			if (Input.GetKey(KeyCode.LeftShift)) {
				transform.position += new Vector3(0f, up*-1, 0f);
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			UpdateCursor();
		}
		if(Input.GetKeyDown(KeyCode.I)){
			UpdateCursor();
		}
	}

	void FixedUpdate(){
		if(!Cursor.visible) {
			float horiz = Input.GetAxis("Horizontal") * speed;
			float vertic = Input.GetAxis("Vertical") * speed;
			Vector3 vector = new Vector3(horiz, 0f, vertic);
			vector = Quaternion.Euler(0f, rotationY, 0f) * vector;
			//rb.AddForce(vector);
			transform.position += vector;
		}
	}

	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void UnLockCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void UpdateCursor(){
		if(Cursor.visible){
			LockCursor();
		} else {
			UnLockCursor();
		}
	}
}
