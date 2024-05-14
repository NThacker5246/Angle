using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureChanger : MonoBehaviour
{
	public BlockEditor be;
	public Slider r;
	public Slider g;
	public Slider b;
	public InputField hex;

	public void SetTexture(){
		be.sel.GetComponent<Renderer>().material.color = new Color32((byte)r.value, (byte)g.value, (byte)b.value, 255);
		string red = ((int)r.value).ToString("X");
		string green = ((int)g.value).ToString("X");
		string blue = ((int)b.value).ToString("X");
		red = ClampString(red);
		green = ClampString(green);
		blue = ClampString(blue);
		hex.text = red + green + blue;
	}

	public string ClampString(string raw){
		if(raw.Length == 2){
			return raw;
		} else {
			return "0" + raw;
		}
	}

	public void SetHex(){
		char[] hex1 = hex.text.ToCharArray();
		char[] red = new char[2];
		char[] green = new char[2];
		char[] blue = new char[2];
		red[0] = hex1[0];
		red[1] = hex1[1];
		green[0] = hex1[2];
		green[1] = hex1[3];
		blue[0] = hex1[4];
		blue[1] = hex1[5];


		int r10 = int.Parse(new string(red), System.Globalization.NumberStyles.HexNumber);
		int g10 = int.Parse(new string(green), System.Globalization.NumberStyles.HexNumber);
		int b10 = int.Parse(new string(blue), System.Globalization.NumberStyles.HexNumber);

		r.value = (float) r10;
		g.value = (float) g10;
		b.value = (float) b10;
		SetTexture();
	}

	public void ReadTexture(){
		if(be.sel.GetComponent<Ids>().mainId == 0 && be.sel.GetComponent<Ids>().groupId == 0) {
			Color32 c = be.sel.GetComponent<Renderer>().material.color;
			r.value = (float) c.r;
			g.value = (float) c.g;
			b.value = (float) c.b;
		}
		SetTexture();
	}
}
