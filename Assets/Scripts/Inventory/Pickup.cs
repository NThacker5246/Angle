using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public GameObject Box;
	public float px;
	public float pz;
	
	public void Drop(){
		Instantiate(Box, transform.position + new Vector3(px, 0f, pz), Quaternion.identity);
		Destroy(gameObject);
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.Q)){
			Drop();
		}
	}
}