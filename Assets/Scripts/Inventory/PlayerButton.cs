using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
	public ActedObject ActionObject;
	public Animator anim;

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			ActionObject.Do();
			anim.SetBool("Click", true);
		}
		Debug.Log(other.tag);
	}

	private void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			ActionObject.StopDoing();
			anim.SetBool("Click", false);
		}
	}
}
