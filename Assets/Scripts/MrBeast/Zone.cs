using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	public MrBeast beast;

	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			beast.isAttack = true;
		}
	}
}
