using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	public bool inCollider;
	public GameObject BoxItem;
	public Transform player;

	private void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			inCollider = true;
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			inCollider = false;
		}
	}

	public void Pickup(){
		if(inCollider){
			Instantiate(BoxItem, player);
			Destroy(this.gameObject);
		}
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.E)){
			Pickup();
		}
	}
}