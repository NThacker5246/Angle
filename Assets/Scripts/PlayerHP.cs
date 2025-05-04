using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
	public float HP;
	public float Stamina;
	public GameManager gm;
	public Slider Bar;
	public Pause stopper;

	public void Damage(float hp){
		HP -= hp;
	}

	public void Heal(float hp){
		HP += hp;
	}

	private void Update(){
		if(HP < 20){
			Heal(Stamina*Time.deltaTime);
		}
		if(HP <= 0f){
			gm.RestartGame();
		}
		if(Bar != null && (int) Bar.value != (int) HP){
			Bar.value = (int) HP;
		}
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			stopper.Stop();
		}
	}
}
