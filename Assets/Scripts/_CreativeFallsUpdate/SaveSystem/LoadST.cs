using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadST : MonoBehaviour
{
	public int loc;
	public Transform player;
    public string mapName;
    // Start is called before the first frame update
    void Start()
    {
        string file_config = ReadNewTextFile("config.json", ".");
        ConfigState config = JsonUtility.FromJson<ConfigState>(file_config);
        string data = ReadNewTextFile(config.currentFile, "./Saves");
        StateOfGame gm = JsonUtility.FromJson<StateOfGame>(data);
        if(loc == 1 && !gm.isReset){
        	player.position = new Vector3(0,0,0);
        	Save();
        	string nc = JsonUtility.ToJson(config);
        	CreateNewTextFile(nc, "config.json", ".");
        } else {
        	player.position = gm.player;
        	mapName = gm.nameMap;
		}
    }

    public void Reset()
    {
        StateOfGame gm = new StateOfGame(new Vector3(0,0,0), "", loc, true);
        string data = JsonUtility.ToJson(gm);
        string file_config = ReadNewTextFile("config.json", ".");
        ConfigState config = JsonUtility.FromJson<ConfigState>(file_config);
        CreateNewTextFile(data, config.currentFile, "./Saves");
    }

    // Update is called once per frame
    public void Save()
    {
    	StateOfGame gm;
    	if(loc == 1){
			gm = new StateOfGame(player.position, "", loc, true);
		} else {
			gm = new StateOfGame(player.position, "", loc, false);
		}
        string data = JsonUtility.ToJson(gm);
        string file_config = ReadNewTextFile("config.json", ".");
        ConfigState config = JsonUtility.FromJson<ConfigState>(file_config);
        CreateNewTextFile(data, config.currentFile, "./Saves");
    }

    public void SaveMap()
    {
        StateOfGame gm = new StateOfGame(player.position, mapName, loc, false);
        string data = JsonUtility.ToJson(gm);
        string file_config = ReadNewTextFile("config.json", ".");
        ConfigState config = JsonUtility.FromJson<ConfigState>(file_config);
        CreateNewTextFile(data, config.currentFile, "./Saves");
    }

	public void CreateNewTextFile(string data, string name, string way)
    {
        using (StreamWriter sw = new StreamWriter(way + "/" + name,false))
	    {
	        sw.Write(data);
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
