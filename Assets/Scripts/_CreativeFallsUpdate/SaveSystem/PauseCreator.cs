using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCreator : MonoBehaviour
{
	public bool isOpenMenu;
	public GameObject Canva;
	public Fade d;
	void PauseResume(){
		Canva.SetActive(!isOpenMenu);
		if(!isOpenMenu){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
		isOpenMenu = !isOpenMenu;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauseResume();
		}
	}

	public void Resume(){
		Time.timeScale = 1;
		Canva.SetActive(false);
		isOpenMenu = false;
	}

	public void EXIT(){
		Time.timeScale = 1;
		d.FadeToLevel();
	}
}
