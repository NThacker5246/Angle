using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContr : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float max;
	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float G;
	private bool flag = true;
	public float kJ;
	private Rigidbody rb;
	public Saver save;
	public Vector3 vc;
	public Vector3 lastPosition;
	public float angularSpeed;
	public bool Get;
	public bool isGrounded;
	[SerializeField] private AudioSource footsteps;
	[SerializeField] private float tm;
	[SerializeField] private float gV;

	[SerializeField] private float posY;
	[SerializeField] private float rayLength = 0.75f; // Adjust based on your character's size
	[SerializeField] private Transform other;
	

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

	public void SavePos(){
		return;
	}

	private void Start(){
		rb = GetComponent<Rigidbody>();
		//transform.position = save.playerPos;
	}
	
	public float Mix(float x, float y, float a){
		return (float) (x * (1.0 - a) + y * a);
	}

	public Vector3 MixV3(Vector3 x, Vector3 y, float a){
		return new Vector3((float) (x.x * (1.0 - a) + y.x * a), (float) (x.y * (1.0 - a) + y.y * a), (float) (x.z * (1.0 - a) + y.z * a));
	}

	private void FixedUpdate(){
		if(Input.GetKey(KeyCode.F)){
			rotationZ += 1f;
			transform.rotation = Quaternion.Euler(0, rotationY, rotationZ);
		}
		if(Input.GetKey(KeyCode.G)){
			rotationZ -= 1f;
			transform.rotation = Quaternion.Euler(0, rotationY, rotationZ);
		}
		float Horizontal = Input.GetAxis("Horizontal")*speed;
		float Vertical = Input.GetAxis("Vertical")*speed;
		//Debug.Log(Vertical);
		
		if(IsGrounded()){
			gV = 0;
			if(Input.GetKey(KeyCode.Space)) gV = 5; 
		} else {
			gV -= G;
		}


		Vector3 movement = new Vector3(Horizontal, gV, Vertical);
		float mouseX = Input.GetAxis("Mouse X") * 150 * Time.deltaTime;
		rotationY += mouseX;
		rotationZ = rotationZ % 360;
		rotationY = rotationY % 360;

		float mouseY = Input.GetAxis("Mouse Y") * 150 * Time.deltaTime;
		rotationX -= mouseY;
		
		float k = rotationZ;
		if(k < 0){
			k = 360 + k;
		}
		k = Mathf.Abs(k - 180);
		k = k - 90;

		k /= 90;
		print(k);

		vc = (new Vector3(rotationY * (1 - Mathf.Abs(k)), rotationY * k, rotationZ));
		//vc = (new Vector3(Mix(rotationY * (1 - Mathf.Abs(k)), rotationX * k, k), Mix(rotationX * (1-Mathf.Abs(k)), rotationY * k, k), rotationZ));

		movement = Quaternion.Euler(new Vector3(rotationY * (1 - Mathf.Abs(k)), rotationY * k, rotationZ)) * movement;
		//rb.AddForce(movement);
		//Vector3 down = new Vector3(0f, -1f*G, 0f);
		//down = Quaternion.Euler(new Vector3(0f, 0f, rotationZ)) * down;
		//MixV3(movement, down, 0.5);
		rb.velocity = movement;

		
	}

	private void Update(){
		/*
		Vector3 down = new Vector3(0f, -1f*G, 0f);
		down = Quaternion.Euler(new Vector3(0f, 0f, rotationZ)) * down;
		if(down != Vector3.zero){
			//rb.AddForce(down);
			rb.velocity += down;
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
			Vector3 up = new Vector3(0f, 2 * kJ *G, 0f);
			up = Quaternion.Euler(new Vector3(0f, 0f, rotationZ)) * up;
			rb.AddForce(up);
		}

		
		*/
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UpdateCursor();
		}
	}


	public bool IsGrounded() {
		Collider[] colliders = Physics.OverlapSphere(other.position, 0.1f); // Adjust radius as needed
		foreach (Collider collider in colliders) {
			if (collider != this.GetComponent<Collider>()) { // Exclude self-collider
				return true;
			}
		}
		return false;
	}
}
