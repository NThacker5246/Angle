using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicableButton : MonoBehaviour
{
	public ActedObject ActionObject;
	public Flyer flyer;
	public bool isColl;
	public Animator anim;
	public bool isFlyer;
	public bool isChecker;
	public int col;

	void Update(){
		if(isColl && Input.GetKeyDown(KeyCode.E) && !isFlyer && !isChecker){
			ActionObject.Do();
			anim.SetTrigger("Click");
			anim.SetTrigger("Click");
		} 
		if(isColl && Input.GetKeyDown(KeyCode.E) && isFlyer){
			flyer.Do();
			anim.SetTrigger("Click");
			anim.SetTrigger("Click");
			Destroy(gameObject);
		}
		if(isColl && Input.GetKeyDown(KeyCode.E) && isChecker){
			flyer.Check(col);
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
