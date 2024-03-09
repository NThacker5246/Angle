using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicableButton : MonoBehaviour
{
	public ActedObject ActionObject;
	public bool isColl;

	void Update(){
		if(isColl && Input.GetKeyDown(KeyCode.E)){
			ActionObject.Do();
		}
	}

	private void OnTriggerEnter(Collider other){
		isColl = true;
	}

	private void OnTriggerExit(Collider other){
		isColl = false;
	}
}
