using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
	public Transform cam;
	public GameObject block;

	public Text FPS;
	public Text position1;

	public bool isShown;

	void Update(){
		if(isShown){
			string pos = "Pos: " + (int)cam.position.x + "/" + (int)cam.position.y + "/" + (int)cam.position.z;
			if(position1.text != pos){
				position1.text = pos;
			}
			FPS.text = "FPS: " + Mathf.Floor(1/Time.deltaTime);
		}

		if(Input.GetKeyDown(KeyCode.F3)){
			isShown = !isShown;
			block.SetActive(isShown);
		}
	}
}
