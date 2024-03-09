using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public ActedObject ActionObject;

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Box"){
			ActionObject.Do();
		}
		
		if(other.tag == "PickedBox") {
			ActionObject.StopDoing();
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.tag == "Box"){
			ActionObject.StopDoing();
		}
	}
}
