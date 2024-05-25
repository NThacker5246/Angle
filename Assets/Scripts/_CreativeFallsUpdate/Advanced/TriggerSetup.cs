using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class TriggerSetup : MonoBehaviour
{
	public InputField id;
	public InputField speed;

	public InputField TX;
	public InputField TY;
	public InputField TZ;

	public InputField NX;
	public InputField NY;
	public InputField NZ;

	public InputField[] ids;

	public BlockEditor be;
	public Transform TriggerSet;

	public GameObject moveT;
	public GameObject aB;

	public void SetTrigger(){
		Trigger trigger = be.sel.GetComponent<Trigger>();
		if(trigger != null){
			if(trigger.Type == "toggle"){
				GameObject gm = be.allBlocks[int.Parse(id.text)];
				trigger.gm = gm;
			} else if(trigger.Type == "move") {
				GameObject gm = be.allBlocks[int.Parse(id.text)];
				trigger.gm = gm;
				Vector3 thisPosition = new Vector3(float.Parse(TX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(TY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(TZ.text, CultureInfo.InvariantCulture.NumberFormat));
				Vector3 newPosition = new Vector3(float.Parse(NX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(NY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(NZ.text, CultureInfo.InvariantCulture.NumberFormat));
				int speed1 = int.Parse(speed.text);

				trigger.thisPosition = thisPosition;
				trigger.newPosition = newPosition;
				trigger.speed = speed1;
			} else if(trigger.Type == "spawn"){
				trigger.triggers = new GameObject[ids.Length];
				int j = 0;
				foreach(InputField id_inp in ids){
					int idI = int.Parse(id_inp.text);
					GameObject block = be.allBlocks[idI];
					trigger.triggers[j] = block;
					j += 1;
				}
			}
		}
	}

	public void ReadTrigger(){
		Trigger trigger = be.sel.GetComponent<Trigger>();
		RemoveInput();
		if(trigger != null){
			if(trigger.Type == "toggle"){
				if(trigger.gm != null) {
					Transform gm = trigger.gm.transform;
					id.text = gm.name;
				}
			} else if(trigger.Type == "move"){
				if(trigger.gm != null) {
					Transform gm = trigger.gm.transform;
					id.text = gm.name;
				}
				speed.text = "" + trigger.speed;

				TX.text = ("" + trigger.thisPosition.x).Replace(",", ".");
				TY.text = ("" + trigger.thisPosition.y).Replace(",", ".");
				TZ.text = ("" + trigger.thisPosition.z).Replace(",", ".");
				
				NX.text = ("" + trigger.newPosition.x).Replace(",", ".");
				NY.text = ("" + trigger.newPosition.y).Replace(",", ".");
				NZ.text = ("" + trigger.newPosition.z).Replace(",", ".");

			} else if(trigger.Type == "spawn"){
				for(int i = 0; i < trigger.triggers.Length-1; i++){
					AddInput();
				}
				int j = 0;
				foreach(GameObject tg in trigger.triggers){
					Transform gm = tg.transform;
					ids[j].text = gm.name;
					j += 1;
				}
			}
		}
	}

	public void AddInput(){
		GameObject gm = id.gameObject;
		GameObject newInput = Instantiate(gm, TriggerSet);
		Transform last = ids[ids.Length-1].transform;
		newInput.transform.position = new Vector3(last.position.x, last.position.y - 60, last.position.z);
		InputField[] temp = ids;
		ids = new InputField[temp.Length + 1];
		int j = 0;
		foreach(InputField inp in temp){
			ids[j] = inp;
			j += 1;
		}
		ids[temp.Length] = newInput.GetComponent<InputField>();
	}

	public void RemoveInput(){
		GameObject temp = id.gameObject;
		foreach(InputField id_inp in ids){	
			GameObject gm = id_inp.gameObject;
			if(gm.transform.name != temp.transform.name) {
				Destroy(gm);
			}
		}
		ids = new InputField[1];
		ids[0] = temp.GetComponent<InputField>();
	}

	public void ShowTg(){
		Trigger trigger = be.sel.GetComponent<Trigger>();
		if(trigger.Type == "move"){
			moveT.SetActive(true);
			aB.SetActive(false);
		} else if (trigger.Type == "spawn") {
			aB.SetActive(true);
			moveT.SetActive(false);
		} else {
			aB.SetActive(false);
			moveT.SetActive(false);
		}
	}
}
