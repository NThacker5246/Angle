using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIComp : MonoBehaviour
{
	public Slider timeToEnd;
	public Image color;
	public int[] cls;
	public int i;
	
	/*
	0 - red
	1 - green
	2 - blue
	*/

	public void Start(){
		for(int i = 0; i < 15; i++){
			cls[i] = ((int) Mathf.Floor(Random.Range(0, 2)));
		}
		switch(cls[i]){
			case 0:
				color.color = new Color32(255, 0, 0, 100);
				break;
			case 1:
				color.color = new Color32(0, 255, 0, 100);
				break;
			case 2:
				color.color = new Color32(0, 0, 255, 100);
				break;
		}
	}

	public void UpdateTimeCounter(float n){
		timeToEnd.value = n;
	}

	public void NextColor(int col){
		if(col == cls[i]){
			i++;
			switch(cls[i]){
				case 0:
					color.color = new Color32(255, 0, 0, 100);
					break;
				case 1:
					color.color = new Color32(0, 255, 0, 100);
					break;
				case 2:
					color.color = new Color32(0, 0, 255, 100);
					break;
			}
		}
	}
}
