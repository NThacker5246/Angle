using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicableButton : MonoBehaviour
{
	public ActedObject ActionObject;
	public bool isColl;
	public Animator anim;

	void Update(){
		if(isColl && Input.GetKeyDown(KeyCode.E)){
			ActionObject.Do();
			anim.SetTrigger("Click");
			anim.SetTrigger("Click");
		}
	}

	private void OnTriggerEnter(Collider other){
		isColl = true;
	}

	private void OnTriggerExit(Collider other){
		isColl = false;
	}
}
