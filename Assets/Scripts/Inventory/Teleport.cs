using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Transform SecondTeleport;
	[SerializeField] private Transform player;
	private Animator anim;
	[SerializeField] private bool isColl;
	[SerializeField] private bool inPortal;

	private void Start(){
		anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			isColl = true; 
			player = other.transform;
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			isColl = false; 
		}
	}

	private void Update(){
		if(isColl && Input.GetKeyDown(KeyCode.R) && !inPortal){
			player.position = SecondTeleport.position;
		}
		if(isColl && Input.GetKeyDown(KeyCode.E)){
			inPortal = !inPortal;
			anim.SetBool("Por", inPortal);
		}
	}
}
