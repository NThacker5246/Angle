using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public MrBeast me;
	public Text dialogueText;
	public Text nameText;

	public Animator boxAnim;
	//public Animator startAnim;
	public bool end;
	private Queue<string> sentences;

	private void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		boxAnim.SetBool("boxOpen", true);
		//startAnim.SetBool("startOpen", false);

		nameText.text = dialogue.name;
		sentences.Clear();

		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		boxAnim.SetBool("boxOpen", false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContr>().UpdateCursor();
		StartCoroutine("Game");
		end = true;
	}

	IEnumerator Game(){
		yield return new WaitForSeconds(1f);
		me.isAttack = true;
	}
}
