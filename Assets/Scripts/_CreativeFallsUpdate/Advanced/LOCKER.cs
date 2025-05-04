using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOCKER : MonoBehaviour
{
	public PlayerContr pl;

	void Update(){
		if(Input.GetKeyDown(KeyCode.I)){ 
			pl.UpdateCursor(); 
		} 
	
	} 
}
