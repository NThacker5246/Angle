using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightChanger : MonoBehaviour
{
	public BlockEditor ed;
	public InputField intensity;
	public InputField range;
	public Slider spotAngle;

	public void SetLight(){
		if(ed.sel.GetComponent<Light>() != null){
			ed.sel.GetComponent<Light>().intensity = int.Parse(intensity.text);
			ed.sel.GetComponent<Light>().range = int.Parse(range.text);
			ed.sel.GetComponent<Light>().spotAngle = spotAngle.value;
		}
	}

	public void GetLight(){
		if(ed.sel.GetComponent<Light>() != null){
			intensity.text = "" + ed.sel.GetComponent<Light>().intensity;
			range.text = "" + ed.sel.GetComponent<Light>().range;
			spotAngle.value = ed.sel.GetComponent<Light>().spotAngle;
		}
	}
}
