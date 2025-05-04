using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameSave : MonoBehaviour
{
	public InputField nameState;

	public InputField nameNew;
	public Dropdown typeofgame;
	public InputField map;

	public SaveToFile loader;
	public PlayerContr player;
	public Fade l0;
	public Fade l1;

	public Text Debugger;

	public void NewState(){
		StateOfGame newState0;
		if(typeofgame.value == 1){
			newState0 = new StateOfGame(new Vector3(0,1,0), map.text, -1, false);
		} else {
			newState0 = new StateOfGame(new Vector3(0,1,0), "", 0, false);
		}

		string json = JsonUtility.ToJson(newState0);
		CreateNewTextFile(json, nameNew.text, "./Saves");
	}


	public void ReadGameState(){
		try {
			StateOfGame newState0;
			string json = ReadNewTextFile(nameState.text, "./Saves");
			//string json = "{\"player\":{\"x\":0.0,\"y\":1.0,\"z\":0.0},\"nameMap\":\"State1.json\",\"standartLevel\":-1}";
			newState0 = JsonUtility.FromJson<StateOfGame>(json);
			if(newState0.standartLevel == -1 && newState0.nameMap != ""){
				Debugger.text = "Debugger: mapper";
				loader.LoadMapUsage(newState0.nameMap, "./Maps", Debugger);
				Debugger.text += ", loaded";
				player.transform.position = newState0.player;
				Debugger.text += ", rester";
				player.G = 2;
				Destroy(gameObject);
				Debugger.text += ", destroyed";
			} else if(newState0.standartLevel == 0){
				ConfigState s = new ConfigState(nameState.text);
				string js = JsonUtility.ToJson(s);
				CreateNewTextFile(js, "config.json", ".");
				l0.FadeToLevel();
			} else if(newState0.standartLevel == 1){
				ConfigState s = new ConfigState(nameState.text);
				string js = JsonUtility.ToJson(s);
				CreateNewTextFile(js, "config.json", ".");
				l1.FadeToLevel();
			}
		} catch(DirectoryNotFoundException e) {
			Debugger.text = "Debugger: " + e;
		}	
	}

	public void CreateNewTextFile(string data, string name, string way)
    {
        using (StreamWriter sw = new StreamWriter(way + "/" + name,false))
	    {
	        sw.WriteLine(data);
	    }

    }
    public string ReadNewTextFile(string name, string way)
    {
        using (StreamReader sr = new StreamReader(way + "/" + name,true))
	    {
	        string line = sr.ReadLine();
	        return line;
	    }

    }
}

public struct StateOfGame{
	public Vector3 player;
	public string nameMap;
	public int standartLevel;
	public bool isReset;

	public StateOfGame(Vector3 pos, string map, int st0, bool rst){
		this.isReset = rst;
		this.player = pos;
		this.nameMap = map;
		this.standartLevel = st0;
	}
}

public struct ConfigState{
	public string currentFile;

	public ConfigState(string name){
		this.currentFile = name;
	}
}