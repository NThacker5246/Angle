using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBeast : MonoBehaviour
{
	[HeaderAttribute ("Stats")]
	public Transform player;
	public float speed;
	public float stopDist;
	public float retreatDist;
	public float hp;

	[HeaderAttribute ("Behaviour")]
	public bool isAttack;
	public bool inColl;

	[HeaderAttribute ("Components")]
	private Animator anim;
	public PlayerHP HP;

	private void Start(){
		anim = GetComponent<Animator>();
	}

	public void MoveToPlayer(){
		float dist = Vector3.Distance(transform.position, player.position);
		Debug.Log(dist);
		if(dist > stopDist){
			transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
		} else if(dist < stopDist && dist > retreatDist){
			return;
		} else if(dist < stopDist && dist < retreatDist){
			transform.position = Vector3.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);
		}
	}

	private void Update(){
		if(isAttack){
			MoveToPlayer();
			anim.SetBool("IsRun", true);
			if(inColl){
				HP.Damage(0.01f);
			}
		} else {
			anim.SetBool("IsRun", false);
		}
	}

	private void OnTriggerEnter(Collider other){
		inColl = true;
	}

	private void OnTriggerExit(Collider other){
		inColl = false;
	}
}
