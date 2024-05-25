using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	public string Type;
	public GameObject gm;

	public Vector3 thisPosition;
	public Vector3 newPosition;
	public int speed;

	public bool isDo;

	public GameObject[] triggers;

	public void OnTriggerEnter(Collider other){
		isDo = true;
	}
	public void OnTriggerExit(Collider other){
		isDo = false;
	}

	void Update(){
		if(isDo) {
			switch (Type)
			{
			    case "move":
			    	Transform obj = gm.GetComponent<Transform>();
			    	obj.position =  Vector3.MoveTowards(obj.position, newPosition, speed*Time.deltaTime);
			        break;
			    case "toggle":
			    	gm.SetActive(false);
			    	break;
			    case "spawn":
			    	foreach(GameObject g in triggers){
			    		Trigger t = g.GetComponent<Trigger>();
			    		t.isDo = true;
			    	}
			    	break;
			}
		} else {
			switch (Type)
			{
			    case "move":
			    	Transform obj = gm.GetComponent<Transform>();
			    	obj.position =  Vector3.MoveTowards(obj.position, thisPosition, speed*Time.deltaTime);
			        break;
			    case "toggle":
			    	gm.SetActive(true);
			    	break;
			    case "spawn":
			    	foreach(GameObject g in triggers){
			    		Trigger t = g.GetComponent<Trigger>();
			    		t.isDo = false;
			    	}
			    	break;
			}
		}
	}
}
