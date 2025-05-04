using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnStarter : MonoBehaviour
{
	public InputField num;
	public BlockEditor editor;
	public Animator anim;

	public void SetUp(){
		int n = int.Parse(num.text);
		//editor.allBlocks = new GameObject[n];
		anim.SetBool("Closed", true);
	}

	public void Close(){
		anim.SetBool("Closed", true);
	}
}
