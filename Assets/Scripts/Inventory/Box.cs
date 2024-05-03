using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	public bool inCollider;
	public GameObject BoxItem;
	public Transform player;
	public PlayerContr pl;

	private void Start(){
		player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			inCollider = true;
			pl = other.GetComponent<PlayerContr>();
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			inCollider = false;
		}
	}

	public void Pickup(){
		if(inCollider && !pl.Get){
			pl.Get = true;
			GameObject gm = Instantiate(BoxItem, player);
			gm.GetComponent<Pickup>().pl = pl;
			Destroy(this.gameObject);
		}
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.E)){
			Pickup();
		}
	}
}