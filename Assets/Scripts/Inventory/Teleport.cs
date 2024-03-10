using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Transform SecondTeleport;
	private Transform player;
	public bool isColl;

	private void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			isColl = true; 
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			isColl = false; 
		}
	}

	private void Update(){
		if(isColl && Input.GetKeyDown(KeyCode.R)){
			player.position = SecondTeleport.position;
		}
	}
}
