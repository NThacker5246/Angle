using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContr : MonoBehaviour
{
	public float speed = 10f;
	public float max;
	public float rotationY;
	public float rotationZ;
	public float G;
	private bool flag = true;
	public float kJ;
	public bool isGrounded;
	private Rigidbody rb;
	public Saver save;
	public Vector3 vc;
	public Vector3 lastPosition;
	public float angularSpeed;
	public bool Get;

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
		save.playerPos = transform.position;
	}

	public float sin(float a){
		float n = a % 180; 
		double rad = (n * 3.1415926535897932384626433832795d)/180;
		double sin = rad - ((rad*rad*rad)/6) + ((rad*rad*rad*rad*rad)/120) - ((rad*rad*rad*rad*rad*rad*rad)/5040) + ((rad*rad*rad*rad*rad*rad*rad*rad*rad)/362880);
		if((a % 360) > 180){
			sin = sin * -1;
		}
		return (float) sin;
	}
	/*
	public float cos(int a){
		int n = a % 181;
		double rad = (n * 3.1415926535897932384626433832795d)/180;
		double cos = 1 - (rad*rad)/2 + ((rad*rad*rad*rad)/24) - ((rad*rad*rad*rad*rad*rad)/720) + ((rad*rad*rad*rad*rad*rad*rad*rad)/40320) - ((rad*rad*rad*rad*rad*rad*rad*rad*rad*rad)/3628800);
		if((a % 180) > 90){
			//cos = cos * -1;
		}
		return (float) cos;
	}
	*/

	public float cos(float a){
		float n = a % 90;
		float cos2 = 1 - Mathf.Pow(sin(n), 2);
		float cos = Mathf.Pow(cos2, 0.5f);
		if((a % 181) > 90){
			//Debug.Log(cos);
			cos = cos * -1;
			//Debug.Log(cos);

		}
		return cos;
	}

	private void Start(){
		rb = GetComponent<Rigidbody>();
		transform.position = save.playerPos;
		Debug.Log(cos(180));
		Debug.Log(Mathf.Cos(180));
	}
	
	private void FixedUpdate(){
		if(Input.GetKey(KeyCode.F)){
			rotationZ += 1f;
			vc = new Vector3(vc.x, vc.y, rotationZ);
		}
		if(Input.GetKey(KeyCode.G)){
			rotationZ -= 1f;
			vc = new Vector3(vc.x, vc.y, rotationZ);
		}
		float Horizontal = Input.GetAxis("Horizontal")*speed;
		float Vertical = Input.GetAxis("Vertical")*speed;
		//Debug.Log(Vertical);
		Vector3 movement = new Vector3(Horizontal, 0f, Vertical);
		float mouseX = Input.GetAxis("Mouse X") * 150 * Time.deltaTime;
		rotationY += mouseX;
		rotationZ = rotationZ % 360;
		rotationY = rotationY % 360;
		
		if(rotationZ > -22 && rotationZ < 22){
			vc = (new Vector3(0f, rotationY, rotationZ));
			movement = Quaternion.Euler(new Vector3(0f, rotationY, rotationZ)) * movement;
		} else if(rotationZ < 45 && rotationZ >= 22){
			vc = (new Vector3(rotationY*0.5f, rotationY*0.5f, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY*0.5f, rotationY*0.5f, rotationZ)) * movement;
		} else if(rotationZ < 135 && rotationZ >= 45){
			vc = (new Vector3(rotationY, rotationY*0, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY, rotationY*0, rotationZ)) * movement;
		} else if(rotationZ < 157 && rotationZ >= 135){
			vc = (new Vector3(rotationY*0.5f, rotationY*-0.5f, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY*0.5f, rotationY*-0.5f, rotationZ)) * movement;
		} else if(rotationZ < -157 || rotationZ >= 157 || rotationZ == 180){
			vc = (new Vector3(rotationY*0, rotationY*-1f, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY*0, rotationY*-1f, rotationZ)) * movement;
		} else if(rotationZ < -135 && rotationZ >= -157){
			vc = (new Vector3(rotationY*-0.5f, rotationY*-0.5f, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY*-0.5f, rotationY*-0.5f, rotationZ)) * movement;
		} else if(rotationZ < -45 && rotationZ >= -135){
			vc = (new Vector3(rotationY*-1f, rotationY*0f, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY*-1f, rotationY*0f, rotationZ)) * movement;
		} else if(rotationZ < -22 && rotationZ >= -45){
			vc = (new Vector3(rotationY*-0.5f, rotationY*0.5f, rotationZ));
			movement = Quaternion.Euler(new Vector3(rotationY*-0.5f, rotationY*0.5f, rotationZ)) * movement;
		} else {
			return;
		}
	
		//transform.eulerAngles = new Vector3(rotationY * sin(rotationZ), rotationY * cos(rotationZ), rotationZ);
		//Debug.Log(sin(rotationZ));
		//Debug.Log(cos(rotationZ));
		//VMC.eulerAngles += new Vector3(0f, mouseX, 0f);
		
		//movement = Quaternion.Euler(new Vector3(rotationY*sin(rotationZ), rotationY*cos(rotationZ), rotationZ)) * movement;
		
		//Debug.Log(movement);
		float angularVelocityX = Mathf.Max(transform.position.x, lastPosition.x) - Mathf.Min(transform.position.x, lastPosition.x);
		float angularVelocityZ = Mathf.Max(transform.position.z, lastPosition.z) - Mathf.Min(transform.position.z, lastPosition.z);
		angularSpeed = angularVelocityX + angularVelocityX;
		if(angularSpeed < max){
			rb.AddForce(movement);
		} 
		//transform.position += movement;
		//vc = transform.position;
		//vc = transform.eulerAngles;
		lastPosition = transform.position;
	}

	private void Update(){
		Vector3 down = new Vector3(0f, -1f*G, 0f);
		down = Quaternion.Euler(new Vector3(0f, 0f, rotationZ)) * down;
		if(down != Vector3.zero){
			rb.AddForce(down);
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
			Vector3 up = new Vector3(0f, 2 * kJ *G, 0f);
			up = Quaternion.Euler(new Vector3(0f, 0f, rotationZ)) * up;
			rb.AddForce(up);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UpdateCursor();
		}
	}
}
