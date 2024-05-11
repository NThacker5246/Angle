using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
			PX.text = "" + (int)sel.position.x;
			PY.text = "" + (int)sel.position.y;
			PZ.text = "" + (int)sel.position.z;
			RX.text = "" + (int)sel.eulerAngles.x;
			RY.text = "" + (int)sel.eulerAngles.y;
			RZ.text = "" + (int)sel.eulerAngles.z;
			SX.text = "" + (int)sel.localScale.x;
			SY.text = "" + (int)sel.localScale.y;
			SZ.text = "" + (int)sel.localScale.z;
			id.text = sel.name;
			ReadAct();
		}
	}
	public void Write(){
		try{
			if(VMc.sel != null && PX.text != ""){
				Transform sel = VMc.sel.transform;
				sel.position = new Vector3(int.Parse(PX.text), int.Parse(PY.text), int.Parse(PZ.text));
				sel.eulerAngles = new Vector3(int.Parse(RX.text), int.Parse(RY.text), int.Parse(RZ.text));
				sel.localScale = new Vector3(int.Parse(SX.text), int.Parse(SY.text), int.Parse(SZ.text));

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
				int tempId = int.Parse(tempIdStr);
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
