using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
	public int state;
	public string[] helpText;
	public Text txt;
	public Animator cloud;
	private Animator an;

	void Start(){
		an = GetComponent<Animator>();
	}

	public void HelpMan(){
		if(state >= helpText.Length){
			cloud.SetBool("cld", false);
			state = 0;
		} else {
			cloud.SetBool("cld", true);
			an.SetBool("help", true);
			StopAllCoroutines();
			//txtSetLetter(helpText[state]);
			StartCoroutine(txtSetLetter(helpText[state]));
			state += 1;
		}
	}

	IEnumerator txtSetLetter(string sentence){
		txt.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			txt.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(2f);
		an.SetBool("help", false);
	}
}
