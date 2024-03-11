using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
	public Animator startAnim;
	public DialogueManager dm;

	public void OnTriggerEnter(Collider other)
	{
		//
		//startAnim.SetBool("startOpen", true);

	}

	public void OnTriggerExit(Collider other)
	{
		//startAnim.SetBool("startOpen", false);
		//dm.EndDialogue();
	}
}
