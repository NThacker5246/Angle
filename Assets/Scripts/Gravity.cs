using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
	public float g;
	private Rigidbody rb;
	public MSCamera MyCamera;

	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	void Update(){
		Vector3 down = new Vector3(0f, -1f*g, 0f);
		down = Quaternion.Euler(0f, 0f, MyCamera.rz) * down;
		Debug.Log(down);
		rb.AddForce(down);
	}
}
