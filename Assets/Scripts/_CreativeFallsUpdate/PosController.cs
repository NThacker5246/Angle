using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class PosController : MonoBehaviour
{
	[SerializeField] private BlockEditor VMc;
	[SerializeField] private Text id;

	[SerializeField] private InputField PX;
	[SerializeField] private InputField PY;
	[SerializeField] private InputField PZ;

	[SerializeField] private InputField RX;
	[SerializeField] private InputField RY;
	[SerializeField] private InputField RZ;

	[SerializeField] private InputField SX;
	[SerializeField] private InputField SY;
	[SerializeField] private InputField SZ;

	[SerializeField] private Text idOfAct;

	[SerializeField] private Animator insp;
	[SerializeField] private Animator butt;
	[SerializeField] private bool isOpen;

	[SerializeField] private TextureChanger tx;
	[SerializeField] private LightChanger lg;
	public TriggerSetup ts;

	public void Read(){
		if(VMc.getSelectedBlock() != null){
			Transform sel = VMc.getSelectedBlock().transform;
			PX.text = ("" + sel.position.x).Replace(",", ".");
			PY.text = ("" + sel.position.y).Replace(",", ".");
			PZ.text = ("" + sel.position.z).Replace(",", ".");
			RX.text = ("" + sel.eulerAngles.x).Replace(",", ".");
			RY.text = ("" + sel.eulerAngles.y).Replace(",", ".");
			RZ.text = ("" + sel.eulerAngles.z).Replace(",", ".");
			SX.text = ("" + sel.localScale.x).Replace(",", ".");
			SY.text = ("" + sel.localScale.y).Replace(",", ".");
			SZ.text = ("" + sel.localScale.z).Replace(",", ".");
			id.text = sel.name;
			ReadAct();
			tx.ReadTexture();
			lg.GetLight();
			ts.ReadTrigger();
		}
	}
	public void Write(){
		try{
			if(VMc.getSelectedBlock() != null && PX.text != ""){
				Transform sel = VMc.getSelectedBlock().transform;
				sel.position = new Vector3(float.Parse(PX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(PY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(PZ.text, CultureInfo.InvariantCulture.NumberFormat));
				sel.eulerAngles = new Vector3(float.Parse(RX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RZ.text, CultureInfo.InvariantCulture.NumberFormat));
				sel.localScale = new Vector3(float.Parse(SX.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SY.text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SZ.text, CultureInfo.InvariantCulture.NumberFormat));

			}
		} catch {
			return;
		}
	}

	public void WriteAct(GameObject other){
		if(VMc.getSelectedBlock() != null){
			ActedObject act = other.GetComponent<ActedObject>();
			if(act != null){
				if(VMc.getSelectedBlock().GetComponent<Button>() != null){
					VMc.getSelectedBlock().GetComponent<Button>().ActionObject = act;
				} else if(VMc.getSelectedBlock().GetComponent<ClicableButton>() != null){
					VMc.getSelectedBlock().GetComponent<ClicableButton>().ActionObject = act;
				} else if(VMc.getSelectedBlock().GetComponent<PlayerButton>() != null){
					VMc.getSelectedBlock().GetComponent<PlayerButton>().ActionObject = act;
				}
			}

			if(VMc.getSelectedBlock().GetComponent<Teleport>()){
				VMc.getSelectedBlock().GetComponent<Teleport>().SecondTeleport = other.transform;
				other.GetComponent<Teleport>().SecondTeleport = VMc.getSelectedBlock().transform;
			}

			VMc.setMode(0);
		}
	}

	public void SetMode(){
		VMc.setMode(1);
	}

	public void ReadAct(){
		try {
			if(VMc.getSelectedBlock() != null){
				if(VMc.getSelectedBlock().GetComponent<Button>() != null){
					idOfAct.text = VMc.getSelectedBlock().GetComponent<Button>().ActionObject.transform.name;
				} else if(VMc.getSelectedBlock().GetComponent<ClicableButton>() != null){
					idOfAct.text = VMc.getSelectedBlock().GetComponent<ClicableButton>().ActionObject.transform.name;
				} else if(VMc.getSelectedBlock().GetComponent<PlayerButton>() != null){
					idOfAct.text = VMc.getSelectedBlock().GetComponent<PlayerButton>().ActionObject.transform.name;
				} else if(VMc.getSelectedBlock().GetComponent<Teleport>()){
					idOfAct.text = VMc.getSelectedBlock().GetComponent<Teleport>().SecondTeleport.name;
				}
			}
		} catch {
			return;
		}
	}

	public void CopyPaste(){
		try{
			if(VMc.getSelectedBlock() != null && PX.text != ""){
				Transform sel = VMc.getSelectedBlock().transform;
				GameObject block = Instantiate(sel.gameObject, sel.position, Quaternion.identity);
				block.transform.eulerAngles = sel.eulerAngles;
				VMc.addBlock(block.name + " (Copy)", block, VMc.getCurrentRoom());
			}
		} catch {
			return;
		}
	}

	public void Remove(){
		try{
			if(VMc.getSelectedBlock() != null){
				Transform sel = VMc.getSelectedBlock().transform;
				VMc.delBlock(sel.name, VMc.getCurrentRoom());
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
