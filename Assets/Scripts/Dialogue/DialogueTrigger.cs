using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue[] dialogue;
	public DialogueManager dm;

	public int Count;

	public void TriggerDialogue()
	{
		dm.StartDialogue(dialogue[Count]);
		dm.isSt = true;
		Count++;
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			TriggerDialogue();
		}
	}
}
