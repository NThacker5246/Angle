using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMan : MonoBehaviour
{

	public AudioSource[] music;
	public AudioSource[] queue;
	// Start is called before the first frame update
	void Start()
	{
		StartMusic(0);
	}

	void Update(){
		foreach(Transform msc in transform){
			AudioSource sfx = msc.gameObject.GetComponent<AudioSource>();
			if (sfx.time > 48f){
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

	public void StartMusic(int i){
		Instantiate(music[i].gameObject, transform);
	}
}
