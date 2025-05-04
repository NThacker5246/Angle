using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBTS : MonoBehaviour
{
	public Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	public void OnClick(){
		StartCoroutine("cl");
	}

	IEnumerator cl(){
		anim.SetBool("Click", true);
		yield return new WaitForSeconds(1.1f);
		anim.SetBool("Click", false);
	}
}
