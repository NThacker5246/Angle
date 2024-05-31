using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
	public PlayerContr player;
	public GameUIComp fun;
	public float step = 0;
	public Teleport tp1;
	public Transform tp2;

	public float rSpeed;
	//private float k = 1;
	private bool b;
	public GameObject msc;

	public void Check(int col){
		if(b){
			fun.NextColor(col);
		}
	}

	public void Do(){
		Debug.Log("ЫЕФКЕАЕЫПЫВ");
		player.G = 0;
		StartCoroutine("Space");
	}

	IEnumerator Space(){
		//Начинаем полёт
		for(int i = 0; i < 600; i++){
			transform.eulerAngles += new Vector3(rSpeed*Time.deltaTime*-1, rSpeed*Time.deltaTime, rSpeed*Time.deltaTime*0.5f);
			yield return new WaitForSeconds(0.1f);
			if(step <= -1f || step >= 1f){
				//k *= -1f;
			}
			//step -= 0.01f*k;
			fun.UpdateTimeCounter(i/5f);
		}

		Debug.Log("Тренировка окончена. Продолжим веселье. Жми кнопки))");
		b = true;
		msc.SetActive(true);
		for(int i = 0; i < 3000; i++){
			transform.eulerAngles += new Vector3(rSpeed*Time.deltaTime*-1, rSpeed*Time.deltaTime, rSpeed*Time.deltaTime*0.5f);
			yield return new WaitForSeconds(0.1f);
			if(step <= -1f || step >= 1f){
				//k *= -1f;
			}
			//step -= 0.01f*k;
			fun.UpdateTimeCounter(i/25f);
		}
		if(fun.i >= 5){
			Debug.Log("Поздравляем, иди в портал на следущий уровень)");
			tp1.SecondTeleport = tp2;
			player.G = 2f;
			msc.SetActive(false);
			StopCoroutine("Space");
		} else{
			Debug.Log("GameOver");
		}
	}
}
