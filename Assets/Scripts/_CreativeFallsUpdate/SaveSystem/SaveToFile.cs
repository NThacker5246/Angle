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
		FileData data = new FileData();
		int k = 0;
		foreach(GameObject object1 in editor.allBlocks){
			if(object1 != null){
				Vector3 pos = object1.transform.position;
				Vector3 rot = object1.transform.eulerAngles;
				Vector3 scl = object1.transform.localScale;
				string id = "";
				if(object1.GetComponent<Button>() != null){
					ActedObject ActionObject = object1.GetComponent<Button>().ActionObject;
					if(ActionObject != null){id = ActionObject.transform.name;}
				} else if(object1.GetComponent<ClicableButton>() != null){
					ActedObject ActionObject = object1.GetComponent<ClicableButton>().ActionObject;
					if(ActionObject != null){id = ActionObject.transform.name;}
				} else if(object1.GetComponent<Teleport>() != null){
					Transform ActionObject = object1.GetComponent<Teleport>().SecondTeleport;
					if(ActionObject != null){id = ActionObject.name;}
				}
				Color32 color1;
				if(object1.GetComponent<Renderer>() != null){
					color1 = object1.GetComponent<Renderer>().material.color;
				} else {
					color1 = new Color32(0, 0, 0, 0);
				}
				float range;
				float spotAngle;
				float intensity;
				if(object1.GetComponent<Light>() != null){
					Light l = object1.GetComponent<Light>();
					range = l.range;
					spotAngle = l.spotAngle;
					intensity = l.intensity;
				} else {
					range = 0;
					spotAngle = 0;
					intensity = 0;
				}
				data.blocks[k] = new Bk(pos, rot, scl, object1.GetComponent<Ids>().groupId, object1.GetComponent<Ids>().mainId, id, color1, intensity, range, spotAngle);
				//data.blocks[k].isSet = true;
				//data.blocks[k].pos = pos;
				//data.blocks[k].rot = rot;
				//data.blocks[k].scl = scl;
				//data.blocks[k].groupId = object1.GetComponent<Ids>().groupId;
				//data.blocks[k].globId = object1.GetComponent<Ids>().mainId;
				//data.blocks[k].linkId = id;
				k += 1;
			}
		}
		//JsonUtility
		Save test = new Save();
		k = 0;
		foreach(Bk blk in data.blocks){
			string dat = JsonUtility.ToJson(blk);
			test.data[k] = dat;
			k += 1;
		}

		string num = JsonUtility.ToJson(test);
		Debug.Log(num);
		Debug.Log(ways[Sway.value] + "\\" + Sname.text);
		//CreateNewTextFile(num, Sname.text, ways[Sway.value]);
		CreateNewTextFile(num, Sname.text, ways[Sway.value]);
	}

	public void Load(){
		Debug.Log(Lway.value);
		string data1 = ReadNewTextFile(Lname.text, ways[Lway.value]);
		Save read = JsonUtility.FromJson<Save>(data1);
		editor.iOfb = 0;
		foreach(string json in read.data){
			Bk realData = JsonUtility.FromJson<Bk>(json);
			if(realData.isSet == true){
				GameObject block = Instantiate(editor.blocks[realData.groupId].blocks[realData.globId], realData.pos, Quaternion.identity);
				block.transform.eulerAngles = realData.rot;
				block.transform.localScale = realData.scl;
				editor.allBlocks[editor.iOfb] = block;
				editor.iOfb += 1;
				block.transform.name = "" + (editor.iOfb-1);
				if(realData.isBlock == true){
					block.GetComponent<Renderer>().material.color = realData.col;
				}
				if(realData.isLight){
					Light l = block.GetComponent<Light>();
					l.intensity = realData.intensity;
					l.range = realData.range;
					l.spotAngle = realData.spotAngle;
				}
			}
		}
		int j = 0;
		foreach(string json in read.data){
			Bk realData = JsonUtility.FromJson<Bk>(json);
			if(realData.linkId != ""){
				int id = int.Parse(realData.linkId);
				if(editor.allBlocks[j].GetComponent<Button>() != null){
					editor.allBlocks[j].GetComponent<Button>().ActionObject = editor.allBlocks[id].GetComponent<ActedObject>();
				} else if(editor.allBlocks[j].GetComponent<ClicableButton>() != null){
					editor.allBlocks[j].GetComponent<ClicableButton>().ActionObject = editor.allBlocks[id].GetComponent<ActedObject>();
				} else if(editor.allBlocks[j].GetComponent<Teleport>() != null){
					editor.allBlocks[j].GetComponent<Teleport>().SecondTeleport = editor.allBlocks[id].GetComponent<Transform>();
				}
			}
			j += 1;
		}
	}

	public void LoadMapUsage(string name, string way, Text deb){
		string data1 = ReadNewTextFile(name, way);
		Save read = JsonUtility.FromJson<Save>(data1);
		//deb.text += ", file"; 
		editor.allBlocks = new GameObject[read.data.Length];
		editor.iOfb = 0;
		//int i=0;
		foreach(string json in read.data){
			//deb.text += ""+i;
			Bk realData = JsonUtility.FromJson<Bk>(json);
			if(realData.isSet == true){
				GameObject block = Instantiate(editor.blocks[realData.groupId].blocks[realData.globId], realData.pos, Quaternion.identity);
				block.transform.eulerAngles = realData.rot;
				block.transform.localScale = realData.scl;
				editor.allBlocks[editor.iOfb] = block;
				editor.iOfb += 1;
				block.transform.name = "" + (editor.iOfb-1);
				if(realData.isBlock == true){
					block.GetComponent<Renderer>().material.color = realData.col;
				}

				if(realData.isLight){
					Light l = block.GetComponent<Light>();
					l.intensity = realData.intensity;
					l.range = realData.range;
					l.spotAngle = realData.spotAngle;
					Destroy(block.GetComponent<Renderer>());
				}
			}
		}
		//deb.text += ", blocked";
		int j = 0;
		foreach(string json in read.data){
			Bk realData = JsonUtility.FromJson<Bk>(json);
			if(realData.linkId != ""){
				int id = int.Parse(realData.linkId);
				if(editor.allBlocks[j].GetComponent<Button>() != null){
					editor.allBlocks[j].GetComponent<Button>().ActionObject = editor.allBlocks[id].GetComponent<ActedObject>();
				} else if(editor.allBlocks[j].GetComponent<ClicableButton>() != null){
					editor.allBlocks[j].GetComponent<ClicableButton>().ActionObject = editor.allBlocks[id].GetComponent<ActedObject>();
				} else if(editor.allBlocks[j].GetComponent<Teleport>() != null){
					editor.allBlocks[j].GetComponent<Teleport>().SecondTeleport = editor.allBlocks[id].GetComponent<Transform>();
				}
			}
			j += 1;
		}
		//deb.text += ", linked";
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
