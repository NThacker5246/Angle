using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
	public PlayerHP pl;
	public bool isCollPl;
	public bool isCollTnt;
	public Vector3 vec;
	public float dmg = 5f;
	public GameObject NearestTNT;
	private Animator anim;

	private void Start(){
		anim = GetComponent<Animator>();
		StartCoroutine("Blown");
	}


	private void OnTriggerEnter(Collider other){
		Debug.Log(other.tag);
		if(other.tag == "Player"){
			pl = other.GetComponent<PlayerHP>();
			isCollPl = true;
		} else if(other.tag == "TNT"){
			NearestTNT = other.gameObject;
			isCollTnt = true;
		}
	}

	private void OnTriggerExit(Collider other){
		Debug.Log(other.tag);
		if(other.tag == "Player"){
			isCollPl = false;
		} else if(other.tag == "TNT"){
			NearestTNT = new GameObject();
			isCollTnt = false;
		}
	}

	IEnumerator Blown(){
		anim.SetTrigger("Nigger");
		yield return new WaitForSeconds(1f);
		if(isCollPl){
			pl.Damage(dmg);
		}

		if(isCollTnt){
			Rigidbody rb = NearestTNT.GetComponent<Rigidbody>();
			rb.AddForce(vec);
		}
		Destroy(gameObject);
	}


}
