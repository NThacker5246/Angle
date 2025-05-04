using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActedObject : MonoBehaviour
{
	public string TypeOfObject;
	public GameObject Box;
	private Animator anim;

	void Start(){
		if(TypeOfObject == "door"){
			anim = GetComponent<Animator>();
		}
	}

	public void Do(){
		if(TypeOfObject == "door"){
			//Debug.Log("Placed");
			anim.SetBool("Door", true);
		}
		if(TypeOfObject == "spawner"){
			Instantiate(Box, transform.position + new Vector3(0f, -5f, 0f), Quaternion.identity);
		}
		if(TypeOfObject == "debug"){
			Debug.Log("Placed");
		}
	}

	public void StopDoing(){
		if(TypeOfObject == "door"){
			//Debug.Log("Placed");
			anim.SetBool("Door", false);
		}
		if(TypeOfObject == "debug"){
			Debug.Log("Picked");
		}
	}
}
