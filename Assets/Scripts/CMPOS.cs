using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPOS : MonoBehaviour
{
	public PlayerContr player;
	void Update(){
		transform.position = player.transform.position;
		transform.eulerAngles = player.vc;
	}
}
