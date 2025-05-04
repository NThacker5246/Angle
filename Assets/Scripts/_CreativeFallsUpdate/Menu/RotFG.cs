using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotFG : MonoBehaviour
{
	public Transform tr;
	public float rotationZ;

	void Update(){
		if(Input.GetKey(KeyCode.F)){
			rotationZ += 1;
		} else if(Input.GetKey(KeyCode.G)){
			rotationZ -= 1;
		}
		if(tr.eulerAngles.z != rotationZ){
			tr.eulerAngles = new Vector3(0f, 0f, rotationZ);
		}
	}
}
