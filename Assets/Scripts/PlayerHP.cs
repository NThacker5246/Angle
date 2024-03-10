using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
	public float HP;
	public float Stamina;
	public GameManager gm;

	public void Damage(float hp){
		HP -= hp;
	}

	public void Heal(float hp){
		HP += hp;
	}

	private void Update(){
		if(HP < 20){
			Heal(Stamina);
		}
		if(HP <= 0f){
			gm.RestartGame();
		}
	}
}
