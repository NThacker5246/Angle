using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveToFile : MonoBehaviour
{
	
	//Add a saving data
	public BlockEditor editor;
	public string[] ways;
	[Header("Save")]
	public InputField Sname;
	public Dropdown Sway;
	[Header("Load")]
	public InputField Lname;
	public Dropdown Lway;

	public void Save(){
		string json = editor.ToJSON();
		CreateNewTextFile(json, Sname.text, ways[Sway.value]);
	}

	public void Load(){
		string json = ReadNewTextFile(Lname.text, ways[Lway.value]);
		editor.WriteDataFromFile(json, 0);
	}

	public void LoadMapUsage(string name, string way, Text deb){
		string json = ReadNewTextFile(name, way);
		editor.WriteDataFromFile(json, 1);
	}

	public void OpenTxt(string data, string file, string way)
    {              
        var lines = File.ReadAllLines(way + "/" + file);
        lines[0] = data;
        File.WriteAllLines(file, lines);
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
