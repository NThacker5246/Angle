using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Vol : MonoBehaviour
{
	public MusicMan msc;
	public Slider sld;
	public int isStartGame;

	void Start(){
		float vol = LoadGM();
		if(vol != -1){
			foreach(Transform musc in msc.transform){
				AudioSource music = musc.GetComponent<AudioSource>();
				music.volume = vol;
			}
		}
	}
	public void Update(){
		if(isStartGame == 0){
			foreach(Transform musc in msc.transform){
				AudioSource music = musc.GetComponent<AudioSource>();
				if(music.volume == sld.value){
					return;
				}

				music.volume = sld.value;
			}
		}
	}

	public void SaveGM(){
		SetGM gm = new SetGM(sld.value);
		string st = JsonUtility.ToJson(gm);
		CreateNewTextFile(st, "gameSet.json");
	}

	public float LoadGM(){;
		string st = ReadNewTextFile("gameSet.json");
		SetGM gm = JsonUtility.FromJson<SetGM>(st);
		if(isStartGame == 0){
			sld.value = gm.volume;
			return -1;
		}
		return gm.volume;
	}


	public void CreateNewTextFile(string data, string name)
    {
        using (StreamWriter sw = new StreamWriter("./" + name,false))
	    {
	        sw.WriteLine(data);
	    }

    }
    public string ReadNewTextFile(string name)
    {
        using (StreamReader sr = new StreamReader("./" + name,true))
	    {
	        string line = sr.ReadLine();
	        return line;
	    }

    }
}

public struct SetGM {
	public float volume;

	public SetGM(float volume){
		this.volume = volume;
	}
}