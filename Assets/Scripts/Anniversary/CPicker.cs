using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CPicker : MonoBehaviour
{
	public Slider br;
	public Slider bg;
	public Slider bb;

	public Slider tr;
	public Slider tg;
	public Slider tb;

	public Image InspectorG;
	public Image StartWin;
	public Image openclose;
	public Text id1;
	public Text id2;
	public Image[] texts;

	void Start(){
		try {
			LoadSettings();
		} finally {
			SaveSettings();
		}
	}

	public void SetColor(){
		Color32 back = new Color32((byte) br.value, (byte) bg.value, (byte) bb.value, 255);
		Color32 text = new Color32((byte) tr.value, (byte) tg.value, (byte) tb.value, 255);
		InspectorG.color = back;
		StartWin.color = back;
		openclose.color = back;
		id1.color = text;
		id2.color = text;
		foreach(Image t in texts){
			t.color = text;
		}

	}

	public void SaveSettings(){
		Color32 back = new Color32((byte) br.value, (byte) bg.value, (byte) bb.value, 255);
		Color32 text = new Color32((byte) tr.value, (byte) tg.value, (byte) tb.value, 255);

		Settings set = new Settings(back, text);
		string st = JsonUtility.ToJson(set);
		CreateNewTextFile(st, "setc.json");

	}

	public void LoadSettings(){
		string st = ReadNewTextFile("setc.json");
		Settings set = JsonUtility.FromJson<Settings>(st);
		br.value = set.back.r;
		bg.value = set.back.g;
		bb.value = set.back.b;

		tr.value = set.text.r;
		tg.value = set.text.g;
		tb.value = set.text.b;
		Color32 back = new Color32((byte) set.back.r, (byte) set.back.g, (byte) set.back.b, 255);
		Color32 text = new Color32((byte) set.text.r, (byte) set.text.g, (byte) set.text.b, 255);

		InspectorG.color = back;
		StartWin.color = back;
		openclose.color = back;
		id1.color = text;
		id2.color = text;
		foreach(Image t in texts){
			t.color = text;
		}

		SetColor();
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

public struct Settings {
	public Color32 back;
	public Color32 text;

	public Settings(Color32 back, Color32 text){
		this.back = back;
		this.text = text;
	}
}