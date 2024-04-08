using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
	public DialogueManager dm;
	public Fade TheRealEnd;

	void Update(){
		if(dm.end){
			TheRealEnd.FadeToLevel();
		}
	}
}
