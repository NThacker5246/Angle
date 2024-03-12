using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vol : MonoBehaviour
{
	public MusicMan msc;
	public Slider sld;
	public void Update(){
		foreach(Transform musc in msc.transform){
			AudioSource music = musc.GetComponent<AudioSource>();
			if(music.volume == sld.value){
				return;
			}

			music.volume = sld.value;
		}
	}
}
