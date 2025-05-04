using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{

	[SerializeField] private float rX;
	[SerializeField] private float rY;
	[SerializeField] private float rZ;

	void Update(){
		if(Input.GetKey(KeyCode.F)){
			if(Input.GetKey(KeyCode.X)){
				rZ += 1f;
				rZ %= 360;
				transform.eulerAngles = new Vector3(rX, rY, rZ);
				return;
			} else if(Input.GetKey(KeyCode.Y)){
				rY += 1f;
				rY %= 360;
				transform.eulerAngles = new Vector3(rX, rY, rZ);
				return;
			}
			rX += 1f;
			rX %= 360;
			transform.eulerAngles = new Vector3(rX, rY, rZ);
		} else if(Input.GetKey(KeyCode.G)){
			if(Input.GetKey(KeyCode.X)){
				rX -= 1f;
				rX %= 360;
				transform.eulerAngles = new Vector3(rX, rY, rZ);
				return;
			} else if(Input.GetKey(KeyCode.Y)){
				rY -= 1f;
				rY %= 360;
				transform.eulerAngles = new Vector3(rX, rY, rZ);
				return;
			}
			rZ -= 1f;
			rZ %= 360;
			transform.eulerAngles = new Vector3(rX, rY, rZ);
		}
	}
}
