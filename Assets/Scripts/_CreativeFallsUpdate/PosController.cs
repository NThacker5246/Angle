using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class PosController : MonoBehaviour
{
	public BlockEditor VMc;
	public Text id;

	public InputField PX;
	public InputField PY;
	public InputField PZ;

	public InputField RX;
	public InputField RY;
	public InputField RZ;

	public InputField SX;
	public InputField SY;
	public InputField SZ;

	public InputField idOfAct;

	public Animator insp;
	public Animator butt;
	public bool isOpen;

	public void Read(){
		if(VMc.sel != null){
			Transform sel = VMc.sel.transform;
			PX.text = "" + (float)sel.position.x;
			PY.text = "" + (float)sel.position.y;
			PZ.text = "" + (float)sel.position.z;
			RX.text = "" + (float)sel.eulerAngles.x;
			RY.text = "" + (float)sel.eulerAngles.y;
			RZ.text = "" + (float)sel.eulerAngles.z;
			SX.text = "" + (float)sel.localScale.x;
			SY.text = "" + (float)sel.localScale.y;
			SZ.text = "" + (float)sel.localScale.z;
			id.text = sel.name;
			ReadAct();
		}
	}
	public void Write(){
		try{
			if(VMc.sel != null && PX.text != ""){
				Transform sel = VMc.sel.transform;
				sel.position = new Vector3(float.Parse(PX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(PY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(PZ.text, CultureInfo.InvariantCulture.NumberFormat));
				sel.eulerAngles = new Vector3(float.Parse(RX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RZ.text, CultureInfo.InvariantCulture.NumberFormat));
				sel.localScale = new Vector3(float.Parse(SX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SZ.text, CultureInfo.InvariantCulture.NumberFormat));

			}
		} catch {
			return;
		}
	}

	public void WriteAct(){
		try{
			if(VMc.sel != null && idOfAct.text != ""){
				Transform sel = VMc.sel.transform;
				int id = int.Parse(idOfAct.text);
				ActedObject act = VMc.allBlocks[id].GetComponent<ActedObject>();
				if (act != null){
					if(sel.GetComponent<Button>() != null){
						sel.GetComponent<Button>().ActionObject = act;
					} else if(sel.GetComponent<ClicableButton>() != null){
						sel.GetComponent<ClicableButton>().ActionObject = act;
					} else if(sel.GetComponent<PlayerButton>() != null){
						sel.GetComponent<PlayerButton>().ActionObject = act;
					}
				}
				if(VMc.allBlocks[id].GetComponent<Teleport>()){
					VMc.allBlocks[id].GetComponent<Teleport>().SecondTeleport = VMc.sel.transform;
					VMc.sel.GetComponent<Teleport>().SecondTeleport = VMc.allBlocks[id].transform;
				}
			}
		} catch {
			return;
		}
	}

	public void ReadAct(){
		try{
			if(VMc.sel != null){
				Transform sel = VMc.sel.transform;
				string s = "";
				if(sel.GetComponent<Button>() != null){
					s = sel.GetComponent<Button>().ActionObject.transform.name;
				} else if(sel.GetComponent<ClicableButton>() != null){
					s = sel.GetComponent<ClicableButton>().ActionObject.transform.name;
				} else if(sel.GetComponent<Teleport>()){
					s = sel.GetComponent<Teleport>().SecondTeleport.name;
				} else if(sel.GetComponent<PlayerButton>() != null){
					s = sel.GetComponent<PlayerButton>().ActionObject.transform.name;
				}
				idOfAct.text = s;
			}

		} catch {
			return;
		}
	}

	public void CopyPaste(){
		try{
			if(VMc.sel != null && PX.text != ""){
				Transform sel = VMc.sel.transform;
				GameObject block = Instantiate(sel.gameObject, sel.position, Quaternion.identity);
	            VMc.allBlocks[VMc.iOfb] = block;
	            VMc.iOfb += 1;
	            block.transform.name = "" + (VMc.iOfb-1);

			}
		} catch {
			return;
		}
	}

	public void Remove(){
		/*
		try{
			if(VMc.sel != null){
				Transform sel = VMc.sel.transform;
				string tempIdStr = sel.name;
				int tempId = float.Parse(tempIdS, CultureInfo.InvariantCulture.NumberFormattr);
				GameObject[] tempArr = VMc.allBlocks;
				GameObject[] newArr = new GameObject[tempArr.Length];
				int i = 0;
				foreach(GameObject gm in tempArr){
					if(i < tempId){
						newArr[i] = gm;
					} else if(i > tempId){
						newArr[i-2] = gm;
					}
					i += 1;
					Debug.Log(i/tempArr.Length*100);
				}

				VMc.allBlocks = newArr;
				Destroy(sel.gameObject);
			}
		} catch {
			return;
		}
		*/
		try{
			if(VMc.sel != null){
				Transform sel = VMc.sel.transform;
				string tempIdStr = sel.name;
				int tempId = int.Parse(tempIdStr);
				VMc.allBlocks[tempId] = null;
				VMc.DeletingBlocks += 1;
				Destroy(sel.gameObject);
			}
		} catch {
			return;
		}

	}

	public void Show(){
		butt.SetBool("ison", true);
		insp.SetBool("isShown", true);
	}
	public void Hide(){
		butt.SetBool("ison", false);
		insp.SetBool("isShown", false);
	}
	public void ShHi(){
		if(!isOpen){
			Show();
		} else {
			Hide();
		}
		isOpen = !isOpen;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.C)){
			ShHi();
		}
	}
}
