using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContr : MonoBehaviour
{
	public float speed = 10f;
	public float rotationY;
	public float rotationZ;
	public float G;
	private bool flag;
	public float kJ;
	public bool isGrounded;
	private Rigidbody rb;

	public void UpdateCursor()
	{
		if(flag == true){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		flag = !flag;
	}

	private void Start(){
		rb = GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate(){
		if(Input.GetKey(KeyCode.F)){
			rotationZ += 1f;
		}
		if(Input.GetKey(KeyCode.G)){
			rotationZ -= 1f;
		}
		float Horizontal = Input.GetAxis("Horizontal")*speed;
		float Vertical = Input.GetAxis("Vertical")*speed;
		//Debug.Log(Vertical);
		Vector3 movement = new Vector3(Horizontal, 0f, Vertical);
		float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
		rotationY += mouseX;
		//transform.eulerAngles = new Vector3(rotationY * Mathf.Sin(rotationZ), rotationY * Mathf.Cos(rotationZ), rotationZ);
		transform.eulerAngles = new Vector3(0f, rotationY, rotationZ);
		//movement = Quaternion.Euler(new Vector3(rotationY*Mathf.Sin(rotationZ), rotationY* Mathf.Cos(rotationZ), rotationZ)) * movement;
		movement = Quaternion.Euler(new Vector3(0f, rotationY, rotationZ)) * movement;
		rb.AddForce(movement);
	}

	private void Update(){
		Vector3 ro = new Vector3(0f, 0f, rotationZ);
		Vector3 down = new Vector3(0f, -1f*G, 0f);
		down = Quaternion.Euler(ro) * down;
		if(down != Vector3.zero){
			rb.AddForce(down);
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
			Vector3 up = new Vector3(0f, 2 * kJ *G, 0f);
			up = Quaternion.Euler(ro) * up;
			rb.AddForce(up);
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			UpdateCursor();
		}
	}
}
