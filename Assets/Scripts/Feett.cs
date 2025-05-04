using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feett : MonoBehaviour
{
	public PlayerContr player;

	void OnTriggerEnter(Collider other){
		if(other.tag != "Player"){
			player.isGrounded = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag != "Player"){
			player.isGrounded = false;
		}
	}

	void LateUpdate(){
		player.isGrounded = false;
	}
}
