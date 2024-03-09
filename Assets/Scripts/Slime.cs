using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
	public GameManager gm;
	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			gm.RestartGame();
		}
	}
}
