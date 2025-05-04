using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
	public Animator Weapon;
	public MrBeast beast;
	public bool inAttColl;
	public GameObject Weapon1;
	public Pause pause;

	void Update(){
		if(Input.GetMouseButtonDown(0) && !pause.isInMenu){
			Weapon1.SetActive(true);
			Debug.Log("Click");
			// Destroy the gameObject after clicking on it
			//Destroy(gameObject);
			Weapon.SetBool("Attack", true);
			if(inAttColl){
				beast.hp -= 5;
			}
			StartCoroutine("Stop");
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "MrBeast"){
			inAttColl = true;
		}
	}

	
	void OnTriggerExit(Collider other){
		if(other.tag == "MrBeast"){
			inAttColl = false;
		}
	}

	IEnumerator Stop(){
		yield return new WaitForSeconds(1f);
		Weapon.SetBool("Attack", false);
		yield return new WaitForSeconds(0.1f);
		Weapon1.SetActive(false);
	}
}
