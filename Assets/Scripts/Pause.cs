using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	//public Animator anim;
	public Transform player;
	public bool isInMenu;
	public GameObject setting;
	//public Saves save;
	//public int Dementions;
	//public Storage st;
	/*
	public void Save(){
		if(Dementions == 2){
			st.PlayerPos2D = new Vector2(player.position.x, player.position.y);
		} else if(Dementions == 3){
			st.PlayerPos3D = new Vector3(player.position.x, player.position.y, player.position.z);
		}
	}
	*/

	public void Stop(){
		isInMenu =  true;
		gameObject.SetActive(true);
		Time.timeScale = 0;
	}

	public void Run(){
		Time.timeScale = 1;
		isInMenu = false;
		gameObject.SetActive(false);
	}

	public void GoToSettings(){
		setting.SetActive(true);
	}

	public void ExitFromSettings(){
		setting.SetActive(false);
	}
}