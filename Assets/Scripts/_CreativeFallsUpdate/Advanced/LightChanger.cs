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
		if(ed.getSelectedBlock().GetComponent<Light>() != null){
			ed.getSelectedBlock().GetComponent<Light>().intensity = int.Parse(intensity.text);
			ed.getSelectedBlock().GetComponent<Light>().range = int.Parse(range.text);
			ed.getSelectedBlock().GetComponent<Light>().spotAngle = spotAngle.value;
		}
	}

	public void GetLight(){
		if(ed.getSelectedBlock().GetComponent<Light>() != null){
			intensity.text = "" + ed.getSelectedBlock().GetComponent<Light>().intensity;
			range.text = "" + ed.getSelectedBlock().GetComponent<Light>().range;
			spotAngle.value = ed.getSelectedBlock().GetComponent<Light>().spotAngle;
		}
	}
}
