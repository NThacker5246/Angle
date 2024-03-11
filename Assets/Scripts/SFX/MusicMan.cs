using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMan : MonoBehaviour
{

	public AudioSource[] music;
	public AudioSource[] queue;
	public float l1;
	public float l2;
	// Start is called before the first frame update
	void Start()
	{
		StartMusic(0);
	}

	void Update(){
		foreach(Transform msc in transform){
			AudioSource sfx = msc.gameObject.GetComponent<AudioSource>();
			if (sfx.time > l1){
				GameObject gm = sfx.gameObject;
				gm.SetActive(false);
				gm.SetActive(true);
			}	
			//Debug.Log(sfx.time);
		}
	}

	public void StopAllMusic(){
		foreach(Transform msc in transform){
			Destroy(msc.gameObject);
		}
	}

	public void EditVoulme(Slider i){
		foreach(AudioSource msc in music){
			msc.volume = i.value;
		}
	}

	public void Replace(){
		music[0] = music[1];
		l1 = l2;
		StopAllMusic();
		StartMusic(0);
	}

	public void StartMusic(int i){
		Instantiate(music[i].gameObject, transform);
	}
}
